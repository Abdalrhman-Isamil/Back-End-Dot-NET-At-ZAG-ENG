-- ? ================= 1NF-Steps-Tables ===================== -- ?

--! 1NF Steps :
--! separate authors into the Authors table
--! separate Genres for Genres table

-- * << Small Note >> * --> : 
--* [ I'm generate this forms tables by AI ] After created them manually in my paper .
-- * ( Thank you for all the efforts Team Leaders ) * -- 

Books Table :

| book_id | book_title               | publisher_id |
| -------  ------------------------    ------------ 
| 1       | Database Systems         | 101          |
| 2       | Clean Code               | 102          |
| 3       | Design Patterns          | 101          |
| 4       | The Pragmatic Programmer | 102          |


BookAuthors Table :

| book_id | author_name   |
| -------  ------------- 
| 1       | Silberschatz  |
| 1       | Korth         |
| 2       | Robert Martin |
| 3       | Gang of Four  |
| 4       | Hunt          |
| 4       | Thomas        |


BookGenres Table :

| book_id | genre     |
| -------  --------- 
| 1       | Computer  |
| 1       | Education |
| 2       | Computer  |
| 3       | Computer  |
| 3       | Reference |
| 4       | Computer  |


Publishers Table :

| publisher_id | publisher_name | publisher_city |
| ------------  --------------  -------------- 
| 101          | McGraw Hill    | New York       |
| 102          | Prentice Hall  | Boston         |


-- ? ==================== 2NF-Steps-Tables ===================== -- ?

--! 2NF Steps : 
--! Every table depends entirely on the primary key only.

--! In the Books Table → each column is based on book_id only 

--! Publishers Table → Each column is based on publisher_id 

--! BookAuthors and BookGenres → Each column is based on the entire :
--!  composite key ( book_id , author_name ) or ( book_id , genre ) 

Books Table :

| Column Name  | Type    | Key |
| ------------  -------   ----- 
| book_id      | int     | PK  |
| book_title   | varchar |     |
| publisher_id | int     | FK  |


Publishers Table :

| Column Name    | Type    | Key |
| --------------   -------   ---  
| publisher_id   | int     | PK  |
| publisher_name | varchar |     |
| publisher_city | varchar |     |


BookAuthors Table :

| Column Name | Type    | Key    |
| -----------   -------   ------  
| book_id     | int     | PK, FK |
| author_name | varchar | PK     |

--! Composite PK = ( book_id , author_name )

BookGenres Table :

| Column Name | Type    | Key    |
| -----------   -------   ------ 
| book_id     | int     | PK, FK |
| genre       | varchar | PK     |


-- ? ==================== 3NF-Steps-Tables ===================== -- ?

--! 2NF Steps : 
--! There are no transitive dependencies

Books Table :

| book_id | book_title | publisher_id |

Authors Table :

| author_id | author_name |

BookAuthors Table :

| book_id | author_id |

Genres Table :

| genre_id | genre_name |

BookGenres Table :

| book_id | genre_id |

Publishers Table :

| publisher_id | publisher_name | publisher_city |
