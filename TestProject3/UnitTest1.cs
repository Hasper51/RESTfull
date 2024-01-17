//using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using RESTfull.Domain;
using RESTfull.Infrastructure;

namespace TestProject3
{
    public class Tests
    {
        /*
        [Fact]

        public void VoidTest()
        {
            var testHelper = new TestHelper();
            var regRepository = testHelper.RegistryRepository;
            Assert.Equal(1, 1);
        }
        */
        [Fact]
        public async void TestAddDisciplines()
        {
            var testHelper = new TestHelper();
            var regRepository = testHelper.RegistryRepository;
            var disciplineRepository = testHelper.DisciplineRepository;
            var registry = await regRepository.GetByNameAsync("IT");

            Assert.NotNull(registry);

            var discipline1 = new Discipline
            {
                Title = "Python",
                Attestation = "Exam",
                Hours = 140,
                RegistryId = registry.Id,
            };
            discipline1.AddSection(new Section { Title = "����������", Content = "a=1" });
            discipline1.AddSection(new Section { Title = "�������", Content = "def F():" });
            
            await disciplineRepository.AddAsync(discipline1);



        }
        /*
        [Fact]
        public async void TestAdd()
        {
            var testHelper = new TestHelper();
            var regRepository = testHelper.RegistryRepository;
            var reg2 = new Registry { Name = "Prog" };

            await regRepository.AddAsync(reg2);

            regRepository.ChangeTrackerClear();

            Assert.Equal(2, regRepository.GetAllAsync().Result.Count);

        }
        
        [Fact]
        public async Task TestUpdateAdd()
        {
            var testHelper = new TestHelper();
            var disciplineRepository = testHelper.DisciplineRepository;

            // �������� ���������� ����������
            var discipline = await disciplineRepository.GetByTitleAsync("���������");

            // ���������, ��� ���������� ������������� ����������
            Assert.NotNull(discipline);

            // �������� ��������
            discipline.Title = "Basics of programming";
            discipline.Attestation = "Exam";
            discipline.Hours = 100;

            // ��������� ����� ������
            var sectionTitle = new Section { Title = "Computers", Content = "Big or small" };
            discipline.AddSection(sectionTitle);

            // ��������� ����������
            await disciplineRepository.UpdateAsync(discipline);

            // �������� �������� ����������� ����������
            var updatedDiscipline = await disciplineRepository.GetByTitleAsync("Basics of programming");

            // ��������� ����������
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
        */

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