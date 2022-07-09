namespace Avaliacao3BimLp3.Models;

class CountStudentGroupByAttribute
{
    public String AttributeName { get; set; }
    public int StudentNumber { get; set; }

    public CountStudentGroupByAttribute() {}

    public CountStudentGroupByAttribute(String attributeName, int studentNumber)
    {
        AttributeName = attributeName;
        StudentNumber = studentNumber; 
    }
}