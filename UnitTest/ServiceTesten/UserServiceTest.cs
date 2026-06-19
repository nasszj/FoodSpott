using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLibrary.Services;
using UnitTest.FakeRepositories;

namespace UnitTest.ServiceTesten
{
    [TestClass]
    public class UserServiceTest
    {
        [TestMethod]
        public void Register_ReturnsTrue_WhenDataIsValid()
        {
            //Arrange
            var service = new UserService(new FakeUserRepository());

            //Act
            var result = service.Register("newuser@gmail.nl", "Welkom123", "Welkom123");

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Register_ReturnsFalse_WhenEmailAlreadyExists()
        {
            //Arrange
            var service = new UserService(new FakeUserRepository());

            //Act
            var result = service.Register("user@gmail.nl", "Welkom123", "Welkom123");

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Register_ReturnsFalse_WhenPasswordIsTooShort()
        {
            //Arrange
            var service = new UserService(new FakeUserRepository());

            //Act
            var result = service.Register("short@gmail.nl", "Welkom", "Welkom");

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Register_ReturnsFalse_WhenPasswordsDoNotMatch()
        {
            //Arrange
            var service = new UserService(new FakeUserRepository());

            //Act
            var result = service.Register("test@gmail.nl", "Welkom123", "Welkom456");

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Login_ReturnsUser_WhenCredentialsAreCorrect()
        {
            //Arrange
            var service = new UserService(new FakeUserRepository());

            //Act
            var result = service.Login("user@gmail.nl", "Welkom123");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("user@gmail.nl", result.Email);
            Assert.AreEqual("User", result.Role);
        }

        [TestMethod]
        public void Login_ReturnsNull_WhenAccountDoesNotExist()
        {
            //Arrange
            var service = new UserService(new FakeUserRepository());

            //Act
            var result = service.Login("unknown@gmail.nl", "Welkom123");

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Login_ReturnsNull_WhenPasswordIsIncorrect()
        {
            //Arrange
            var service = new UserService(new FakeUserRepository());

            //Act
            var result = service.Login("user@gmail.nl", "WrongPassword123");

            //Assert
            Assert.IsNull(result);
        }
    }
}