namespace TestProject2
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]

        public void VoidTest()
        {
            var testHelper = new TestHelper();
            var disciplineRepository = testHelper.DisciplineRepository;
            Assert.Equal(1, 1);
        }

        [Test]
        public void TestUpdateAdd()
        {
            var testHelper = new TestHelper();
            var disciplineRepository = testHelper.DisciplineRepository;
            var discipline = disciplineRepository.GetByTitleAsync("Math").Result;
            disciplineRepository.ChangeTrackerClear();
            discipline.Title = "Informatics";
            discipline.Attestation = "Exam";
            discipline.Hours = 100;
            var sectionTitle = new Section { Title = "Computers", Content = "Big or small" };
            discipline.AddSection(sectionTitle);
            disciplineRepository.UpdateAsync(discipline).Wait();
            Assert.Equals("Informatics", disciplineRepository.GetByTitleAsync("Informatics").Result.Title);
            Assert.Equal(3, disciplineRepository.GetByTitleAsync("Informatics").Result.DisciplineCount);
        }

        [Test]
        public void TestUpdateDelete()
        {
            var testHelper = new TestHelper();
            var disciplineRepository = testHelper.DisciplineRepository;
            var discipline = disciplineRepository.GetByTitleAsync("Math").Result;
            disciplineRepository.ChangeTrackerClear();
            discipline.RemoveAt(0);
            disciplineRepository.UpdateAsync(discipline).Wait();
            Assert.Equal(1, disciplineRepository.GetByTitleAsync("Math").Result.DisciplineCount);
        }

    }
}