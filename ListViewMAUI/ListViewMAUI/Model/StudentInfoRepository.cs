using System.Collections.ObjectModel;

namespace ListViewMAUI.Model
{
    internal class StudentInfoRepository
    {
        public StudentInfoRepository() 
        {
            
        }

        public ObservableCollection<StudentInfo> GetStudentDetails(int count)
        {
            ObservableCollection<StudentInfo> sudentDetails = new();
            int girlsCount = 0, boysCount = 0;
            for (int i = 0; i < count; i++)
            {
                var details = new StudentInfo()
                {
                    Image = "people_circle" + (i % 19) + ".png"
                };

                if (imagePosition.Contains(i % 19))
                    details.Name = Student_Boys[boysCount++ % 32];
                else
                    details.Name = Student_Girls[girlsCount++ % 93];

                details.RollNum = (i + 1000).ToString();
                sudentDetails.Add(details);
            }
            return sudentDetails;
        }

        public List<string> GetStudentNames(int count)
        {
            List<string> studentNames = new();

            int girlsCount = 0, boysCount = 0;
            for (int i = 0; i < count; i++)
            {
                if (imagePosition.Contains(i % 19))
                    studentNames.Add(Student_Boys[boysCount++ % 32]);
                else
                    studentNames.Add(Student_Girls[girlsCount++ % 93]);
            }
            return studentNames;
        }

        public List<string> GetStudentsRollNum(int count)
        {
            List<string> studentsRollNum = new();
            for (int i = 0; i < count; i++)
            {
                studentsRollNum.Add((i+1000).ToString());
            }
            return studentsRollNum;
        }

        readonly int[] imagePosition = new int[]
        {
            5,
            8,
            12,
            14,
            18
        };

        readonly string[] Student_Girls = new string[]
        {
            "Kyle",
            "Gina",
            "Brenda",
            "Danielle",
            "Fiona",
            "Lila",
            "Jennifer",
            "Liz",
            "Pete",
            "Katie",
            "Vince",
            "Fianna",
            "Liam   ",
            "Georgia",
            "Elijah ",
            "Alivia",
            "Evan   ",
            "Ariel",
            "Vanessa",
            "Gabriel",
            "Angelina",
            "Eli    ",
            "Remi",
            "Levi",
            "Alina",
            "Layla",
            "Ella",
            "Mia",
            "Emily",
            "Clara",
            "Lily",
            "Melanie",
            "Rose",
            "Brianna",
            "Bailey",
            "Juliana",
            "Valerie",
            "Hailey",
            "Daisy",
            "Sara",
            "Victoria",
            "Grace",
            "Layla",
            "Josephine",
            "Jade",
            "Evelyn",
            "Mila",
            "Camila",
            "Chloe",
            "Zoey",
            "Nora",
            "Ava",
            "Natalia",
            "Eden",
            "Cecilia",
            "Finley",
            "Trinity",
            "Sienna",
            "Rachel",
            "Sawyer",
            "Amy",
            "Ember",
            "Rebecca",
            "Gemma",
            "Catalina",
            "Tessa",
            "Juliet",
            "Zara",
            "Malia",
            "Samara",
            "Hayden",
            "Ruth",
            "Kamila",
            "Freya",
            "Kali",
            "Leiza",
            "Myla",
            "Daleyza",
            "Maggie",
            "Zuri",
            "Millie",
            "Lilliana",
            "Kaia",
            "Nina",
            "Paislee",
            "Raelyn",
            "Talia",
            "Cassidy",
            "Rylie",
            "Laura",
            "Gracelynn",
            "Heidi",
            "Kenzie",
        };

        readonly string[] Student_Boys = new string[]
        {
            "Irene",
            "Watson",
            "Ralph",
            "Torrey",
            "William",
            "Bill",
            "Howard",
            "Daniel",
            "Frank",
            "Jack",
            "Oscar",
            "Larry",
            "Holly",
            "Steve",
            "Zeke",
            "Aiden",
            "Jackson",
            "Mason",
            "Jacob  ",
            "Jayden ",
            "Ethan  ",
            "Noah   ",
            "Lucas  ",
            "Brayden",
            "Logan  ",
            "Caleb  ",
            "Caden  ",
            "Benjamin",
            "Xaviour",
            "Ryan   ",
            "Connor ",
            "Michael",
        };
    }
}
