CREATE OR REPLACE FUNCTION get_user_by_login(_login VARCHAR)
    RETURNS TABLE(id INT, login VARCHAR, password VARCHAR, name VARCHAR, surname VARCHAR, age INT) AS $$
BEGIN
    RETURN QUERY
        SELECT users.id, users.login, users.password, users.name, users.surname, users.age
        FROM users
        WHERE users.login = _login;
END;
$$ LANGUAGE plpgsql;