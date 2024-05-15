using RegisterOfStudents.Models;

namespace Loyhalarim.Broker.Storeage
{
    internal class ListStoreageBroker : IStoreageBroker
    {
        private List<Student> students = new List<Student>();

        public ListStoreageBroker()
        {
            students.Add(new Student()
            {
                Id = 1,
                FirstName = "Inomjon",
                LastName = "Safarov",
                Age = 16,
                Email = "Safarov@gmail.com"
            });
            students.Add(new Student()
            {
                Id = 2,
                FirstName = "Odil",
                LastName = "Qadirov",
                Age = 18,
                Email = "Qodirov@gmail.com"
            });
            students.Add(new Student()
            {
                Id = 3,
                FirstName = "Shohjahon",
                LastName = "Safarov",
                Age = 27,
                Email = "Saidov@gmail.com"
            });
            students.Add(new Student()
            {
                Id = 4,
                FirstName = "Alisher",
                LastName = "Jovliyev",
                Age = 27,
                Email = "Jovliyev@gmail.com"
            });
            students.Add(new Student()
            {
                Id = 5,
                FirstName = "Diyorbek",
                LastName = "Ravshanov",
                Age = 27,
                Email = "Ravshanov@gmail.com"
            });
        }

        public Student AddStudent(Student student)
        {
            foreach (Student studentItem in students)
            {
                if (studentItem.Id == student.Id
                    && studentItem.Email == student.Email)
                {
                    return new Student();
                }
            }
            students.Add(student);
            return student;
        }

        public Student FindStudentByName(string firstName)
        {
            foreach (Student studentItem in students)
            {
                if (studentItem.FirstName == firstName)
                {
                    return studentItem;
                }
            }
            return new Student();
        }

        public List<Student> FindStudentByLetter(char letter)
        {
            List<Student> studentNew = new List<Student>();
            foreach (Student studentItem in students)
            {
                if (studentItem.FirstName.Contains(letter))
                {
                    studentNew.Add(studentItem);
                }
            }
            return studentNew;
        }

        public DemoStudent PrintNameAndEmail(int id)
        {
            foreach (Student studentItem in students)
            {
                if (studentItem.Id == id)
                {
                    var studentInfo = new DemoStudent()
                    {
                        FirstName = studentItem.FirstName,
                        Email = studentItem.Email
                    };
                    return studentInfo;
                }
            }
            return new DemoStudent();
        }
    }
}
