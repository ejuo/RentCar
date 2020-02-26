using NSubstitute;
using NUnit.Framework;
using RentCar.Domain;
using RentCar.Domain.Interfaces;
using RentCar.Domain.Services.PreflightService;
using System.Threading.Tasks;

namespace RentCar.Tests
{
  public class Tests
  {
    private IPreflightService _preflightService;

    private IBookingRepository _bookingRepository;

    [SetUp]
    public void Setup()
    {
      _bookingRepository = Substitute.For<IBookingRepository>();

      _preflightService = new PreflightService(_bookingRepository);
    }

    [Test]
    public async Task ShouldBlockedIfRented()
    {
      _bookingRepository.HasRentAsync(Arg.Any<int>()).Returns(true);

      var car = new Car { Name = "TestCar" };

      var result = await _preflightService.Preflight(car);

      Assert.AreEqual(PreflightResultCode.BLOCKED, result.Code);
      
      Assert.AreEqual(string.Format(PreflightResultMessage.BLOCKED, car.Name), result.Message);

      StringAssert.DoesNotContain(string.Format(PreflightResultMessage.APPROVE, car.Name), result.Message);
    }

    [Test]
    public async Task ShouldWarningAccelerationIfRented()
    {
      _bookingRepository.HasRentAsync(Arg.Any<int>()).Returns(false);

      var car = new Car { Name = "TestCar", AccelerationTo100Secs = 15, CarType = new CarType { AccelerationTo100Secs = 5 } };

      var result = await _preflightService.Preflight(car);

      Assert.AreEqual(PreflightResultCode.WARNING, result.Code);

      StringAssert.Contains(PreflightResultMessage.ACCELERATION, result.Message);

      StringAssert.Contains(string.Format(PreflightResultMessage.APPROVE, car.Name), result.Message);
    }

    [Test]
    public async Task ShouldOkAccelerationRoundedIfRented()
    {
      _bookingRepository.HasRentAsync(Arg.Any<int>()).Returns(false);

      var car = new Car { Name = "TestCar", AccelerationTo100Secs = 6, CarType = new CarType { AccelerationTo100Secs = 5 } };

      var result = await _preflightService.Preflight(car);

      Assert.AreEqual(PreflightResultCode.OK, result.Code);

      StringAssert.DoesNotContain(PreflightResultMessage.ACCELERATION, result.Message);

      StringAssert.Contains(string.Format(PreflightResultMessage.APPROVE, car.Name), result.Message);
    }

    [Test]
    public async Task ShouldWarningRadioIfRented()
    {
      _bookingRepository.HasRentAsync(Arg.Any<int>()).Returns(false);

      var car = new Car { Name = "TestCar", Radio = false, CarType = new CarType { Radio = true } };

      var result = await _preflightService.Preflight(car);

      Assert.AreEqual(PreflightResultCode.WARNING, result.Code);

      StringAssert.Contains(PreflightResultMessage.RADIO, result.Message);

      StringAssert.Contains(string.Format(PreflightResultMessage.APPROVE, car.Name), result.Message);
    }

    [Test]
    public async Task ShouldWarningAirConditionerIfRented()
    {
      _bookingRepository.HasRentAsync(Arg.Any<int>()).Returns(false);

      var car = new Car { Name = "TestCar", AirConditioner = false, CarType = new CarType { AirConditioner = true } };

      var result = await _preflightService.Preflight(car);

      Assert.AreEqual(PreflightResultCode.WARNING, result.Code);

      StringAssert.Contains(PreflightResultMessage.AIRCONDITIONER, result.Message);

      StringAssert.Contains(string.Format(PreflightResultMessage.APPROVE, car.Name), result.Message);
    }

    [Test]
    public async Task ShouldWarningRadioAndAirConditionerIfRented()
    {
      _bookingRepository.HasRentAsync(Arg.Any<int>()).Returns(false);

      var car = new Car { Name = "TestCar", Radio = false, AirConditioner = false, CarType = new CarType { Radio = true, AirConditioner = true } };

      var result = await _preflightService.Preflight(car);

      Assert.AreEqual(PreflightResultCode.WARNING, result.Code);

      StringAssert.Contains(PreflightResultMessage.AIRCONDITIONER, result.Message);
      StringAssert.Contains(PreflightResultMessage.RADIO, result.Message);

      StringAssert.Contains(string.Format(PreflightResultMessage.APPROVE, car.Name), result.Message);
    }

    [Test]
    public async Task ShouldOkIfRented()
    {
      _bookingRepository.HasRentAsync(Arg.Any<int>()).Returns(false);

      var car = new Car { Name = "TestCar", Radio = true, AirConditioner = true, AccelerationTo100Secs = 6, 
        CarType = new CarType { Radio = true, AirConditioner = true, AccelerationTo100Secs = 5 } };

      var result = await _preflightService.Preflight(car);

      Assert.AreEqual(PreflightResultCode.OK, result.Code);

      StringAssert.DoesNotContain(PreflightResultMessage.AIRCONDITIONER, result.Message);
      StringAssert.DoesNotContain(PreflightResultMessage.RADIO, result.Message);
      StringAssert.DoesNotContain(PreflightResultMessage.ACCELERATION, result.Message);

      StringAssert.Contains(string.Format(PreflightResultMessage.APPROVE, car.Name), result.Message);
    }
  }
}