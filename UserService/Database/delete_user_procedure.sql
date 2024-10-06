CREATE OR REPLACE PROCEDURE delete_user_by_id(_id INT)
    LANGUAGE plpgsql AS $$
BEGIN
    DELETE FROM users WHERE id = _id;
END;
$$;