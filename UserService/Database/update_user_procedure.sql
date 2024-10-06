CREATE OR REPLACE PROCEDURE update_user_data(_id INT, _password VARCHAR, _name VARCHAR, _surname VARCHAR, _age INT)
    LANGUAGE plpgsql AS $$
BEGIN
    UPDATE users
    SET password = _password, name = _name, surname = _surname, age = _age
    WHERE id = _id;
END;
$$;