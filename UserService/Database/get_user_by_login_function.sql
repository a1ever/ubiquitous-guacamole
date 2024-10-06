CREATE OR REPLACE FUNCTION get_user_by_login(_login VARCHAR)
    RETURNS TABLE(id INT, login VARCHAR, password VARCHAR, name VARCHAR, surname VARCHAR, age INT) AS $$
BEGIN
    RETURN QUERY
        SELECT id, login, password, name, surname, age
        FROM users
        WHERE login = _login;
END;
$$ LANGUAGE plpgsql;