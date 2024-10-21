using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;
using System.Text;

namespace SPLab6
{
    public partial class Form1 : Form
    {
        BindingList<Student> studentsList = new BindingList<Student>();
        private TcpListener _server;
        private Thread _listenThread;
        private volatile bool _isRunning;

        public Form1()
        {
            InitializeComponent();
            loadData();
            StartServer();
            
        }

        private void loadData()
        {
            
            studentsList = new BindingList<Student>
            {
                new ("Василий", "123", false),
                new ("Иннокентий", "234", false),
                new ("Варфоломей", "345", false),
            };

            
            studentsGridView.DataSource = studentsList;
            
        }

        private void StartServer()
        {
            _isRunning = true;
            _server = new TcpListener(IPAddress.Any, 8888);
            _server.Start();
            _listenThread = new Thread(ListenForClients);
            _listenThread.Start();
        }

        private void ListenForClients()
        {
            while (_isRunning)
            {
                try
                {
                    TcpClient client = _server.AcceptTcpClient();
                    Thread clientThread = new Thread(HandleClientComm);
                    clientThread.Start(client);
                    studentsGridView.Invalidate();
                }
                catch (SocketException) 
                { }
            }    
        }

        private void HandleClientComm(object clientObj)
        {
            TcpClient tcpClient = (TcpClient)clientObj;
            NetworkStream stream = tcpClient.GetStream();

            

            byte[] buffer = new byte[1024];
            int bytesRead;

            

            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                string studentID = Encoding.ASCII.GetString(buffer, 0, bytesRead).Trim();
                var student = studentsList.FirstOrDefault(s => s.studentId == studentID);
                if (student != null)
                {
                   student.isPresent = true;
                  
                }
                break;
            }

            tcpClient.Close();
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
           _isRunning = false;
           _server.Stop();
           base.OnFormClosing(e);
        }


    }

    public class Student
    {
        public string fullname { get; set; }
        public string studentId { get; set; }
        public bool isPresent { get; set; }

        public Student(string fullname, string studentId, bool isPresent)
        {
            this.fullname = fullname; 
            this.studentId = studentId;
            this.isPresent = isPresent;
        }
    }

}
