﻿CREATE OR REPLACE FUNCTION get_user_by_name_surname(_name VARCHAR, _surname VARCHAR)
    RETURNS TABLE(id INT, login VARCHAR, password VARCHAR, name VARCHAR, surname VARCHAR, age INT) AS $$
BEGIN
    RETURN QUERY SELECT * FROM users WHERE users.name = _name AND users.surname = _surname;
END;
$$ LANGUAGE plpgsql;