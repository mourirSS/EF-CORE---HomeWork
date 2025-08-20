﻿using MyApp.Repositories;
using MyApp.Menus; //менюшки гпт помогал писать.

var studentRepo    = new StudentRepository();
var teacherRepo    = new TeacherRepository();
var courseRepo     = new CourseRepository();
var enrollmentRepo = new EnrollmentRepository();

var mainMenu = new MainMenu(studentRepo, teacherRepo, courseRepo, enrollmentRepo);
mainMenu.Run();
