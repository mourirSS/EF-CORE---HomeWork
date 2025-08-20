INSERT INTO Teachers (FullName, Email) VALUES
('Irina Smirnova', 'i.smirnova@school.local'),
('Oleg Petrov', 'o.petrov@school.local'),
('Anna Ivanova', 'a.ivanova@school.local');

-- Courses
INSERT INTO Courses (CourseName, Credits, TeacherId) VALUES
('Databases', 5, 1),   -- ведёт Irina
('Algorithms', 4, 2),  -- ведёт Oleg
('C# Basics', 3, 1),   -- ведёт Irina
('Networks', 4, 3);    -- ведёт Anna

-- Students
INSERT INTO Students (FullName, BirthDate) VALUES
('Ali Veliyev', '2002-03-12'),
('Nigar Mammadova', '2003-11-05'),
('Vusal Karimov', '2001-07-21'),
('Leyla Huseynova', '2002-09-30');

-- Enrollments (student-course links)
INSERT INTO Enrollments (StudentId, CourseId, Grade) VALUES
(1, 1, 95),   -- Ali → Databases
(1, 3, 66),   -- Ali → C# Basics
(2, 2, 88),   -- Nigar → Algorithms
(2, 4, 99),   -- Nigar → Networks
(3, 1, 76),   -- Vusal → Databases
(4, 2, 91);   -- Leyla → Algorithms