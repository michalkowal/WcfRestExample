using NSubstitute;
using NUnit.Framework;
using WcfRestExample.Common.Infrastructure;
using WcfRestExample.Plugins.Logger;

namespace WcfResExample.UnitTests
{
    [TestFixture]
    public class LoggerPluginTest
    {
        private ILoggerExt _mockLogger;
        private LoggerService _sut;

        [SetUp]
        public void Init()
        {
            _mockLogger = Substitute.For<ILoggerExt>();

            _sut = new LoggerService(_mockLogger);
        }

        [Test]
        public void BaseRouteTest()
        {
            Assert.AreEqual("logger", _sut.BaseRoute);
        }

        [Test]
        public void DebugMethodTest()
        {
            LogDTO mock = new LogDTO() { Module = "LoggerServiceTest", Function = "MethodTest", Message = "Test message" };
            _sut.Debug(mock);

            _mockLogger.Received(1).Debug(Arg.Is<string>(s => s.Contains(mock.Module) && s.Contains(mock.Function) && s.Contains(mock.Message) && s.Contains("127.0.0.1")));
        }

        [Test]
        public void ErrorMethodTest()
        {
            LogDTO mock = new LogDTO() { Module = "LoggerServiceTest", Function = "MethodTest", Message = "Test message" };
            _sut.Error(mock);

            _mockLogger.Received(1).Error(Arg.Is<string>(s => s.Contains(mock.Module) && s.Contains(mock.Function) && s.Contains(mock.Message) && s.Contains("127.0.0.1")));
        }

        [Test]
        public void FatalMethodTest()
        {
            LogDTO mock = new LogDTO() { Module = "LoggerServiceTest", Function = "MethodTest", Message = "Test message" };
            _sut.Fatal(mock);

            _mockLogger.Received(1).Fatal(Arg.Is<string>(s => s.Contains(mock.Module) && s.Contains(mock.Function) && s.Contains(mock.Message) && s.Contains("127.0.0.1")));
        }

        [Test]
        public void InfoMethodTest()
        {
            LogDTO mock = new LogDTO() { Module = "LoggerServiceTest", Function = "MethodTest", Message = "Test message" };
            _sut.Info(mock);

            _mockLogger.Received(1).Info(Arg.Is<string>(s => s.Contains(mock.Module) && s.Contains(mock.Function) && s.Contains(mock.Message) && s.Contains("127.0.0.1")));
        }

        [Test]
        public void TraceMethodTest()
        {
            LogDTO mock = new LogDTO() { Module = "LoggerServiceTest", Function = "MethodTest", Message = "Test message" };
            _sut.Trace(mock);

            _mockLogger.Received(1).Trace(Arg.Is<string>(s => s.Contains(mock.Module) && s.Contains(mock.Function) && s.Contains(mock.Message) && s.Contains("127.0.0.1")));
        }

        [Test]
        public void WarnMethodTest()
        {
            LogDTO mock = new LogDTO() { Module = "LoggerServiceTest", Function = "MethodTest", Message = "Test message" };
            _sut.Warn(mock);

            _mockLogger.Received(1).Warn(Arg.Is<string>(s => s.Contains(mock.Module) && s.Contains(mock.Function) && s.Contains(mock.Message) && s.Contains("127.0.0.1")));
        }
    }
}
