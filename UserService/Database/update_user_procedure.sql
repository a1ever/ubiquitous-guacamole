CREATE OR REPLACE FUNCTION update_user_data(
    _id INT,
    _password VARCHAR,
    _name VARCHAR,
    _surname VARCHAR,
    _age INT
) RETURNS BOOLEAN
    LANGUAGE plpgsql AS $$
DECLARE
    rows_affected INT;
BEGIN
    UPDATE users
    SET password = _password,
        name = _name,
        surname = _surname,
        age = _age
    WHERE id = _id;

    GET DIAGNOSTICS rows_affected = ROW_COUNT;

    IF rows_affected > 0 THEN
        RETURN TRUE;
    ELSE
        RETURN FALSE;
    END IF;
EXCEPTION
    WHEN OTHERS THEN
        RETURN FALSE;
END;
$$;
