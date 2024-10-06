CREATE TABLE users (
                       id SERIAL PRIMARY KEY ,
                       login VARCHAR(255) UNIQUE NOT NULL,
                       password VARCHAR(255) NOT NULL,
                       name VARCHAR(255),
                       surname VARCHAR(255),
                       age INT
);
