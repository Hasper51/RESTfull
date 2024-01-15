//using Microsoft.AspNetCore.Routing;
using RESTfull.Domain;

namespace TestProject3
{
    public class Tests
    {
        [Fact]

        public void VoidTest()
        {
            var testHelper = new TestHelper();
            var regRepository = testHelper.RegistryRepository;
            Assert.Equal(1, 1);
        }

        [Fact]
        public async void TestAdd()
        {
            var testHelper = new TestHelper();
            var regRepository = testHelper.RegistryRepository;
            var reg2 = new Registry { Name = "" };

            await regRepository.AddAsync(reg2);
            regRepository.ChangeTrackerClear();

            Assert.Equal(2, regRepository.GetAllAsync().Result.Count);

        }

        [Fact]
        public async Task TestUpdateAdd()
        {
            var testHelper = new TestHelper();
            var disciplineRepository = testHelper.DisciplineRepository;

            // Получаем дисциплину асинхронно
            var discipline = await disciplineRepository.GetByTitleAsync("Math");

            // Убедитесь, что дисциплина действительно существует
            Assert.NotNull(discipline);

            // Изменяем атрибуты
            discipline.Title = "Basics of programming";
            discipline.Attestation = "Exam";
            discipline.Hours = 100;

            // Добавляем новый раздел
            var sectionTitle = new Section { Title = "Computers", Content = "Big or small" };
            discipline.AddSection(sectionTitle);

            // Обновляем дисциплину
            await disciplineRepository.UpdateAsync(discipline);

            // Повторно получаем обновленную дисциплину
            var updatedDiscipline = await disciplineRepository.GetByTitleAsync("Basics of programming");

            // Проверяем обновления
            Assert.Equal("Basics of programming", updatedDiscipline.Title);
            Assert.Equal(3, updatedDiscipline.Sections.Count);
        }

        [Fact]
        public void TestUpdateDelete()
        {
            var testHelper = new TestHelper();
            var disciplineRepository = testHelper.DisciplineRepository;
            var discipline = disciplineRepository.GetByTitleAsync("Math").Result;
            disciplineRepository.ChangeTrackerClear();
            discipline.RemoveAt(0);
            disciplineRepository.UpdateAsync(discipline).Wait();
            Assert.Equal(1, disciplineRepository.GetByTitleAsync("Math").Result.Sections.Count);
        }
            
    }
}


/*
        public void TestUpdateAdd()
        {
            var testHelper = new TestHelper();
            var disciplineRepository = testHelper.DisciplineRepository;
            var discipline = disciplineRepository.GetByTitleAsync("Math").Result;
            disciplineRepository.ChangeTrackerClear();
            discipline.Title = "Basics of programming";
            discipline.Attestation = "Exam";
            discipline.Hours = 100;
            var sectionTitle = new Section { Title = "Computers", Content = "Big or small" };
            discipline.AddSection(sectionTitle);
            disciplineRepository.UpdateAsync(discipline).Wait();
            Assert.Equal("Basics of programming", disciplineRepository.GetByTitleAsync("Basics of programming").Result.Title);
            Assert.Equal(3, disciplineRepository.GetByTitleAsync("Basics of programming").Result.Sections.Count);
        }
*/