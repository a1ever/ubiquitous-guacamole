CREATE OR REPLACE PROCEDURE create_user(_login VARCHAR, _password VARCHAR, _name VARCHAR, _surname VARCHAR, _age INT)
    LANGUAGE plpgsql AS $$
BEGIN
    INSERT INTO users (login, password, name, surname, age)
    VALUES (_login, _password, _name, _surname, _age);
END;
$$;