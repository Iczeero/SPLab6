using System.ComponentModel;

namespace SPLab6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            BindingList<Student> present = new BindingList<Student>();
            Student vasa = new Student("Вася Компотный", "123", false);
            present.Add(vasa);
            studentsGridView.DataSource = present;
            studentBindingSource.DataSource = present;
        }

        
    }

    public class Student
    {
        string fullname;
        string number;
        bool isPresent;

        public Student(string fullname, string number, bool isPresent)
        {
            this.fullname = fullname;
            this.number = number;
            this.isPresent = isPresent;
        }
    }

}
