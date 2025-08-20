CREATE TABLE Teachers (
    TeacherId   INT IDENTITY(1,1) PRIMARY KEY,
    FullName    NVARCHAR(100) NOT NULL,
    Email       NVARCHAR(100) NULL
);

-- Курсы
CREATE TABLE Courses (
    CourseId    INT IDENTITY(1,1) PRIMARY KEY,
    CourseName       NVARCHAR(100) NOT NULL,
    Credits     INT NOT NULL,
    TeacherId   INT NOT NULL FOREIGN KEY REFERENCES Teachers(TeacherId)
);
-- Студенты
CREATE TABLE Students (
    StudentId   INT IDENTITY(1,1) PRIMARY KEY,
    FullName    NVARCHAR(100) NOT NULL,
    BirthDate   DATE NOT NULL
);

-- Зачисления (связь студент-курс)
CREATE TABLE Enrollments (
    EnrollmentId INT IDENTITY(1,1) PRIMARY KEY,
    StudentId    INT NOT NULL FOREIGN KEY REFERENCES Students(StudentId),
    CourseId     INT NOT NULL FOREIGN KEY REFERENCES Courses(CourseId),
    Grade        INT NOT NULL
);