

INSERT INTO Users (userName, password, email, isEmailConfirmed)
VALUES
('john_doe', HASHBYTES('SHA2_256', 'Pass123!'), 'john.doe@example.com', 1),
('alice_smith', HASHBYTES('SHA2_256', 'SecurePass1'), 'alice.smith@example.com', 1),
('michael_jones', HASHBYTES('SHA2_256', 'Test12345'), 'michael.jones@example.com', 0),
('emily_clark', HASHBYTES('SHA2_256', 'StrongPass!'), 'emily.clark@example.com', 1),
('david_wilson', HASHBYTES('SHA2_256', 'MySecret98'), 'david.wilson@example.com', 0),
('sophia_taylor', HASHBYTES('SHA2_256', 'Password123!'), 'sophia.taylor@example.com', 1),
('james_brown', HASHBYTES('SHA2_256', 'JamesSecurePass1'), 'james.brown@example.com', 1),
('olivia_harris', HASHBYTES('SHA2_256', 'OliviaPass321'), 'olivia.harris@example.com', 0),
('william_moore', HASHBYTES('SHA2_256', 'WilliamP@ss99'), 'william.moore@example.com', 1),
('ava_anderson', HASHBYTES('SHA2_256', 'Anderson_123!'), 'ava.anderson@example.com', 0),
-- Добавь ещё 40 пользователей аналогичным образом
('charlotte_king', HASHBYTES('SHA2_256', 'Charl0tteK!ng98'), 'charlotte.king@example.com', 1);

-- Вставка ролей
INSERT INTO Roles (roleName)
VALUES ('Admin'), ('User'), ('Moderator');

-- Назначение ролей пользователям
INSERT INTO UserRoles (userNameRef, roleNameRef)
VALUES
('john_doe', 'Admin'),
('alice_smith', 'User'),
('michael_jones', 'Moderator'),
('emily_clark', 'User'),
('david_wilson', 'User'),
('sophia_taylor', 'Admin'),
('james_brown', 'User'),
('olivia_harris', 'Moderator'),
('william_moore', 'User'),
('ava_anderson', 'Admin');
-- Добавь оставшиеся роли для пользователей


select * from Users;