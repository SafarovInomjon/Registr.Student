


using RegisterOfStudents.Models;

namespace RegisterOfStudents.Broker.Storeage
{
    internal interface IStoreageBroker
    {
        DemoStudent PrintNameAndEmail(int id);
        List<Student> FindStudentByLetter(char letter);
        Student FindStudentByName(string firstName);
        Student AddStudent(Student student);
    }
}
