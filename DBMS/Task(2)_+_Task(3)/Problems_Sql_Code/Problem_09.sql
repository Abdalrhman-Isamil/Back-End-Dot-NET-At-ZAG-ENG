-- ? ================= 1NF-Steps-Tables ===================== -- ?

--! 1NF Steps :
--! Separate students from courses → Enrollments table
--! Separate instructors from multiple columns → CourseInstructors table

-- * << Small Note >> * --> : 
--* [ I'm generate this forms tables by AI ] After created them manually in my paper .
-- * ( Thank you for all the efforts Team Leaders ) * -- 

Students Table :

| student_id | student_name | student_email                         |
| 1001       | Alice Brown  | [alice@uni.edu](mailto:alice@uni.edu) |
| 1002       | Bob Chen     | [bob@uni.edu](mailto:bob@uni.edu)     |
| 1003       | Carol Davis  | [carol@uni.edu](mailto:carol@uni.edu) |



Courses Table :

| course_id |
| CS101     |
| CS102     |
| MATH201   |



Enrollments Table :

| student_id | course_id |
| 1001       | CS101     |
| 1001       | CS102     |
| 1002       | MATH201   |
| 1003       | CS101     |
| 1003       | MATH201   |



CourseInstructors Table :

| course_id | instructor_name | instructor_dept  | dept_building  |
| CS101     | Dr. Smith       | Computer Science | Tech Hall      |
| CS102     | Dr. Lee         | Computer Science | Tech Hall      |
| MATH201   | Dr. Johnson     | Mathematics      | Science Center |



-- ? ==================== 2NF-Steps-Tables ===================== -- ?

--! 2NF Steps : 
--! Each column is based on the student_id .

--! Enrollments Table → Each column is based on (student_id, course_id) → Okay

--! Enrollments Table → Each column depends on ( student_id , course_id ).
--! CourseInstructors Table → Each column depends on ( course_id , instructor_name ).


Students Table :

| Column Name   | Type    | Key |
| student_id    | int     | PK  |
| student_name  | varchar |     |
| student_email | varchar |     |



Courses Table :

| Column Name | Type    | Key |
| course_id   | varchar | PK  |



Enrollments Table :

| Column Name | Type    | Key    |
| student_id  | int     | PK, FK |
| course_id   | varchar | PK, FK |


CourseInstructors Table: 

| Column Name     | Type    | Key    |
| course_id       | varchar | PK, FK |
| instructor_name | varchar | PK     |
| instructor_dept | varchar |        |
| dept_building   | varchar |        |


--! Composite PK = ( book_id , author_name )

BookGenres Table :

| Column Name | Type    | Key    |
| book_id     | int     | PK, FK |
| genre       | varchar | PK     |


-- ? ==================== 3NF-Steps-Tables ===================== -- ?

--! 2NF Steps : 
--! There are no transitive dependencies

Departments Table :

| dept_name        | dept_building  |
| Computer Science | Tech Hall      |
| Mathematics      | Science Center |


CourseInstructors Table :

| course_id | instructor_name | dept_name        |
| CS101     | Dr. Smith       | Computer Science |
| CS102     | Dr. Lee         | Computer Science |
| MATH201   | Dr. Johnson     | Mathematics      |

Students :

| student_id | student_name | student_email |

Courses :

| course_id |

Enrollments : 

| student_id | course_id |

Departments :

| dept_name | dept_building |

CourseInstructors :

| course_id | instructor_name | dept_name |
