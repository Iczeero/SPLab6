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
        BindingList<Student> present = new BindingList<Student>();
        private TcpListener _server;
        private Thread _listenThread;

        public Form1()
        {
            InitializeComponent();
            loadData();
            
            StartServer();
        }

        private void loadData()
        {
            
            present = new BindingList<Student>
            {
                new ("Вася Компотный", "123", false),
                
            };

            
            studentsGridView.DataSource = present;
            //studentsGridView.Refresh();
            //studentsGridView.
        }

        private void StartServer()
        {
            _server = new TcpListener(IPAddress.Any, 8888);
            _server.Start();
            _listenThread = new Thread(ListenForClients);
            _listenThread.Start();
        }

        private void ListenForClients()
        {
            while (true)
            {
                TcpClient client = _server.AcceptTcpClient();
                Thread clientThread = new Thread(HandleClientComm);
                clientThread.Start(client);
            }
        }

        private void HandleClientComm(object clientObj)
        {
            TcpClient tcpClient = (TcpClient)clientObj;
            NetworkStream stream = tcpClient.GetStream();

            byte[] buffer = new byte[1024];
            int bytesRead;

            //MessageBox.Show("Подключено");

            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                string studentID = Encoding.ASCII.GetString(buffer, 0, bytesRead).Trim();

                // Поиск студента по номеру зачетки
                var student = present.FirstOrDefault(s => s.studentId == studentID);
                if (student != null)
                {
                   student.isPresent = true; // Отметить студента как присутствующего
            //        Invoke(new Action(() =>
            //        {
            //            //txtMessages.AppendText($"Студент {student.Name} ({student.ID}) отмечен как присутствующий.\n");
            //        }));
                }
            //    else
            //    {
            //        Invoke(new Action(() =>
            //        {
            //            //txtMessages.AppendText($"Студент с номером зачетки {studentID} не найден.\n");
            //        }));
            //    }

                // Закрыть соединение
                break;
            }

            tcpClient.Close();
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
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
