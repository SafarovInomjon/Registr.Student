



using Loyhalarim.Broker.Storeage;
using Loyhalarim.Broker;
using RegisterOfStudents.Models;

namespace RegisterOfStudents.Service
{
    internal class StudentService : IStudentService
    {
        private readonly ILoggingBroker loggingBroker;
        private readonly IStoreageBroker storeageBroker;
        public StudentService()
        {
            this.loggingBroker = new LoggingBroker();
            this.storeageBroker = new ListStoreageBroker();
        }

        public List<Student> CheckoutByLetter(char letter)
        {
            return letter.ToString() is null
                ? InvalidCheckoutByLetter()
                : ValidationCheckoutByLetter(letter);
        }

        public Student CheckoutByName(string firstName)
        {
            return firstName is null
                ? InvalidCheckoutByName()
                : ValidationAndCheckoutByName(firstName);
        }

        public DemoStudent DisplayStudent(int id)
        {
            return id is 0
                ? InvalidDisplayStudent()
                : ValidationAndDisplayStudent(id);

        }

        public Student InsertStudent(Student student)
        {
            return student is null
                ? InsertStudentInvalid()
                : ValidationAndInsertStudent(student);
        }

        private Student ValidationAndInsertStudent(Student student)
        {
            if (student.Id is 0
                || String.IsNullOrWhiteSpace(student.FirstName)
                || String.IsNullOrWhiteSpace(student.LastName)
                || student.Age is 0
                || String.IsNullOrWhiteSpace(student.Email))
            {
                this.loggingBroker.LogError("Invalid student information.");
                return new Student();
            }
            else
            {
                var studentInformation = this.storeageBroker.AddStudent(student);
                if (studentInformation.Email is not null)
                {
                    this.loggingBroker.LogInformation("Succssesfull.");
                }
                else
                {
                    this.loggingBroker.LogError("Not Added.");
                }
                return studentInformation;
            }
        }

        private Student InvalidCheckoutByName()
        {
            this.loggingBroker.LogError("The firstname is invalid.");
            return new Student();
        }

        private List<Student> ValidationCheckoutByLetter(char letter)
        {
            List<Student> studenInfo = this.storeageBroker.FindStudentByLetter(letter);
            if (studenInfo.Count is not 0)
            {
                foreach (var student in studenInfo)
                {
                    this.loggingBroker.LogInformation($"Id: {student.Id}\n" +
                            $"FirstName: {student.FirstName}\nLastName: {student.LastName}\n" +
                            $"Age: {student.Age}\nEmail: {student.Email}");
                }
            }
            else
            {
                this.loggingBroker.LogError("The user for the entered letter does not exist.");
            }
            return studenInfo;
        }

        private List<Student> InvalidCheckoutByLetter()
        {
            this.loggingBroker.LogError("Not Found.");
            return new List<Student>();
        }

        private Student ValidationAndCheckoutByName(string firstName)
        {
            if (String.IsNullOrWhiteSpace(firstName))
            {
                this.loggingBroker.LogError("The information is not fully formed.");
                return new Student();
            }
            else
            {
                var studentInfo = this.storeageBroker.FindStudentByName(firstName);
                if (studentInfo.Email is not null)
                {
                    this.loggingBroker.LogInformation($"Reference found.\nId: {studentInfo.Id}\n" +
                        $"FirstName: {studentInfo.FirstName}\nLastName: {studentInfo.LastName}\n" +
                        $"Age: {studentInfo.Age}\nEmail: {studentInfo.Email}");
                }
                else
                {
                    this.loggingBroker.LogError("Not Found.");
                }

                return studentInfo;
            }
        }

        private DemoStudent ValidationAndDisplayStudent(int id)
        {
            var studentInfo = this.storeageBroker.PrintNameAndEmail(id);
            if (studentInfo.FirstName is not null)
            {
                this.loggingBroker.LogInformation($"FirstName: {studentInfo.FirstName}\n" +
                    $"Email: {studentInfo.Email}");
                return studentInfo;
            }
            else
            {
                this.loggingBroker.LogError("No information found.");
                return new DemoStudent();
            }
        }

        private DemoStudent InvalidDisplayStudent()
        {
            this.loggingBroker.LogError("Invalid Id.");
            return new DemoStudent();
        }

        private Student InsertStudentInvalid()
        {
            this.loggingBroker.LogError("Student info is null.");
            return new Student();
        }
    }

    internal interface IStudentService
    {
    }
}