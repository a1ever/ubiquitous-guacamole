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
    -- Выполняем UPDATE и сохраняем количество затронутых строк
    UPDATE users
    SET password = _password,
        name = _name,
        surname = _surname,
        age = _age
    WHERE id = _id;

    -- Получаем количество затронутых строк
    GET DIAGNOSTICS rows_affected = ROW_COUNT;

    -- Если хотя бы одна строка обновлена, возвращаем TRUE, иначе FALSE
    IF rows_affected > 0 THEN
        RETURN TRUE;
    ELSE
        RETURN FALSE;
    END IF;
EXCEPTION
    WHEN OTHERS THEN
        -- В случае ошибки возвращаем FALSE
        RETURN FALSE;
END;
$$;
