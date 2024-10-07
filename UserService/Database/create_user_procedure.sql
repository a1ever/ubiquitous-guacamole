CREATE OR REPLACE FUNCTION create_user(
    _login VARCHAR,
    _password VARCHAR,
    _name VARCHAR,
    _surname VARCHAR,
    _age INT
) RETURNS BOOLEAN
    LANGUAGE plpgsql AS $$
BEGIN
    INSERT INTO users (login, password, name, surname, age)
    VALUES (_login, _password, _name, _surname, _age);

    RETURN TRUE;
EXCEPTION
    WHEN others THEN
        RETURN FALSE;
END;
$$;
