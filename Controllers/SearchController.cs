using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Test.Models;

namespace Test.Controllers
{
	public class SearchController : Controller
	{
		private readonly ILogger<TimetableController> _logger;
		private readonly AppDbContext _context;
		public SearchController(ILogger<TimetableController> logger, AppDbContext context)
		{
			_logger = logger;
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}
		// Action tra ve url chuyen huong den view co tham so chua Id cua nguoi dung bang mot chuoi query string
		[HttpGet]
		public IActionResult UserSearch(string searchData)
		{
			_logger.LogInformation("UserSearch action");
			User? User = _context.Users.FromSqlInterpolated
				(
				$@"SELECT * 
					FROM [Test].[dbo].[Users] 
					WHERE [UserName] = {searchData}"
				).FirstOrDefault();
			if(User != null)
			{
				string url = Url.Action("DisplayUser") + $"?userId={User.Id}";
				return Json(url);
			}
			else
			{
				return Json("No user found");
			}
		}
		// Action tra ve view co thong tin ve cac tkb cua nguoi dung 
		public IActionResult DisplayUser(string userId)
		{
			_logger.LogInformation("DisplayUser action");
			List<PTimetable> Timetables = _context.Timetable.FromSqlInterpolated
				(
				$@"SELECT * 
					FROM [Test].[dbo].[Timetable] AS t
					WHERE t.[UserId] = (
						SELECT u.[Id] 
						FROM [Test].[dbo].[Users] AS u
						WHERE u.[Id] = {userId}
						)
					"
				).ToList();
			User? User = _context.Users.FromSqlInterpolated($@"
				SELECT *
				FROM [Test].[dbo].[Users]
				WHERE [Id] = {userId}
				").FirstOrDefault();
			ViewBag.Timetables = Timetables;
			ViewBag.User = User?.UserName;
			return View();
		}
		// Lop QueriedClass de hung cac thuoc tinh can thiet khi query tu csdl
		public class QueriedClass
		{
			public int ClassId { set; get; }
			public string? SubjectId { set; get; }
			public string? SubjectName { set; get; }
			public string? ClassType { set; get; }
			public string? ClassRoom { set; get; }
			public int Weekday { set; get; }
			public string? Time { set; get; }

		}
		// Action tra ve view the hien thoi khoa bieu co id bang timetableId
		public IActionResult DisplayTable(int timetableId)
		{
			// Truy van tu bang ClassTb de lay ra tat ca cac ma lop co thuoc tinh TimetableId trung voi timetableId
			List<ClassTb> ClassTb = _context.ClassTb.FromSqlInterpolated
				(
				$@"SELECT *
					FROM [Test].[dbo].[ClassTb] AS ct
					WHERE ct.[TimetableId] = {timetableId}"
				).ToList();
			List<QueriedClass> ResultClass = new List<QueriedClass>();
			// Tu ma lop hoc truy van ra thong tin cua lop hoc do
			foreach (var ClassCell in ClassTb)
			{
				// Truy van lay ra thong tin lop hoc
				Class? QueryClass = _context.Class.FromSqlInterpolated
					(
					$@"SELECT * 
						FROM [Test].[dbo].[Class]
						WHERE [ClassId] = {ClassCell.ClassId}"
					).FirstOrDefault();
				// Truy van lay ra mon hoc cua lop hoc
				Subject? QuerySubject = _context.Subject.FromSqlInterpolated
					(
					$@"SELECT *
						FROM [Test].[dbo].[Subject]
						WHERE [SubjectId] = {QueryClass.SubjectId}"
					).FirstOrDefault();
				// Truy van lay ra thoi gian cua lop hoc
				List<ClassTime> QueryClassTime = _context.ClassTime.FromSqlInterpolated
					(
					$@"SELECT *
						FROM [Test].[dbo].[ClassTime]
						WHERE [ClassId] = {ClassCell.ClassId}"
					).ToList();
				// 1 lop hoc co the co nhieu thoi gian hoc (nhieu buoi hoc) -> su dung vong lap de luu vao tat ca cac thoi gian do
				foreach (var QueriedClassTime in QueryClassTime)
				{
					var Class = new QueriedClass()
					{
						ClassId = ClassCell.ClassId,
						SubjectId = QuerySubject.SubjectId,
						SubjectName = QuerySubject.SubjectName,
						ClassType = QueryClass.ClassType,
						ClassRoom = QueriedClassTime.ClassRoom,
						Weekday = QueriedClassTime.Weekday,
						Time = QueriedClassTime.Time
					};
					ResultClass.Add(Class);
				}
			}
			// Chuyen hoa List<QueriedClass> thanh dang du lieu json
			var JsonResult = JsonConvert.SerializeObject(ResultClass);
			ViewBag.ResultClass = JsonResult;
			return View();
		}
		// Action tra ve url chuyen huong den view co cac thoi khoa bieu theo ki hoc
		public IActionResult TermSearch(int searchData)
		{
			var Timetables = _context.Timetable.FromSqlInterpolated(
				$@"SELECT *
					FROM [Test].[dbo].[Timetable]
					WHERE [Term] = {searchData}"
				).FirstOrDefault();
			
			if(Timetables != null)
			{
				string url = Url.Action("DisplayTerm") + $"?Term={searchData}";
				return Json(url);
			}
			return Json("No timetable found");
		}
		// Action tra ve view co cac thong tin ve thoi khoa bieu theo ki hoc
		public IActionResult DisplayTerm(int Term)
		{
			var Timetables = _context.Timetable.FromSqlInterpolated(
				$@"SELECT *
					FROM [Test].[dbo].[Timetable]
					WHERE [Term] = {Term}"
				).ToList();
			var UserNames = _context.Users.FromSqlInterpolated(
				$@"SELECT *
					FROM [Test].[dbo].[Users]
					WHERE [Id] IN 
						(
							SELECT DISTINCT [UserId]
							FROM [Test].[dbo].[Timetable]
							WHERE [Term] = {Term}
						)"
				).ToList();
			ViewBag.Timetables = Timetables;
			ViewBag.UserNames = UserNames;
			return View();
		}
	}
}
