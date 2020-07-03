


CREATE TABLE Carrito 
    (
     id BIGINT NOT NULL IDENTITY(1,1), 
     created_at DATETIME , 
     closed_at DATETIME , 
     id_usuario BIGINT NOT NULL 
    )
GO

ALTER TABLE Carrito ADD CONSTRAINT Carrito_PK PRIMARY KEY CLUSTERED (id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE Carrito_Cartas 
    (
     id BIGINT NOT NULL IDENTITY(1,1), 
     id_carrito BIGINT NOT NULL , 
     id_carta BIGINT NOT NULL , 
     cantidad BIGINT 
    )
GO

ALTER TABLE Carrito_Cartas ADD CONSTRAINT Carrito_Cartas_PK PRIMARY KEY CLUSTERED (id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE Carta_ingredientes 
    (
     id BIGINT NOT NULL IDENTITY(1,1), 
     id_carta BIGINT NOT NULL , 
     id_ingrediente BIGINT NOT NULL , 
     cantidad_grs BIGINT 
    )
GO

ALTER TABLE Carta_ingredientes ADD CONSTRAINT Carta_ingredientes_PK PRIMARY KEY CLUSTERED (id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE Cartas 
    (
     id BIGINT NOT NULL IDENTITY(1,1), 
     nombre VARCHAR (80) , 
     descripcion VARCHAR (100) , 
     precio_unidad VARCHAR (100) , 
     created_at DATETIME , 
     id_foto BIGINT , 
     id_promocion BIGINT 
    )
GO

ALTER TABLE Cartas ADD CONSTRAINT Cartas_PK PRIMARY KEY CLUSTERED (id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE Delivery 
    (
     id BIGINT NOT NULL IDENTITY(1,1), 
     id_repartidor BIGINT NOT NULL , 
     id_usuario BIGINT , 
     id_venta BIGINT , 
     id_estado BIGINT NOT NULL 
    )
GO

ALTER TABLE Delivery ADD CONSTRAINT Delivery_PK PRIMARY KEY CLUSTERED (id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE Estados 
    (
     id BIGINT NOT NULL IDENTITY(1,1), 
     nombre VARCHAR (30) , 
     created_at DATETIME , 
     deleted_at DATE 
    )
GO

ALTER TABLE Estados ADD CONSTRAINT Estados_PK PRIMARY KEY CLUSTERED (id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE Fotos 
    (
     id BIGINT NOT NULL IDENTITY(1,1), 
     nombre VARCHAR (80) NOT NULL , 
     url VARCHAR (800) NOT NULL , 
     created_at DATETIME 
    )
GO

ALTER TABLE Fotos ADD CONSTRAINT Fotos_PK PRIMARY KEY CLUSTERED (id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE Ingredientes 
    (
     id BIGINT NOT NULL IDENTITY(1,1), 
     nombre VARCHAR (80) , 
     descripcion VARCHAR (500) 
    )
GO

ALTER TABLE Ingredientes ADD CONSTRAINT Ingredientes_PK PRIMARY KEY CLUSTERED (id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE Promociones 
    (
     id BIGINT NOT NULL IDENTITY(1,1), 
     nombre VARCHAR (100) NOT NULL , 
     descripcion VARCHAR (500) NOT NULL , 
     porc_descuento BIGINT NOT NULL , 
     max_descuento BIGINT , 
     created_at DATETIME , 
     expired_at DATETIME 
    )
GO

ALTER TABLE Promociones ADD CONSTRAINT Promociones_PK PRIMARY KEY CLUSTERED (id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE Repartidor 
    (
     id BIGINT NOT NULL IDENTITY(1,1), 
     nombre VARCHAR (80) , 
     apellidos VARCHAR (80) , 
     celular VARCHAR (15) , 
     rut VARCHAR (10) , 
     dv VARCHAR (1) , 
     email VARCHAR (30) 
    )
GO

ALTER TABLE Repartidor ADD CONSTRAINT Repartidor_PK PRIMARY KEY CLUSTERED (id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE Usuario 
    (
     id BIGINT NOT NULL IDENTITY(1,1), 
     rut VARCHAR (10) , 
     dv VARCHAR (1) , 
     nombre VARCHAR (80) , 
     apellidos VARCHAR (80) , 
     direccion VARCHAR (80) , 
     email VARCHAR (50) , 
     celular VARCHAR (15) , 
     password VARCHAR (30) , 
     created_at DATETIME , 
     deleted_at DATETIME 
    )
GO

ALTER TABLE Usuario ADD CONSTRAINT Usuario_PK PRIMARY KEY CLUSTERED (id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE Venta 
    (
     id BIGINT NOT NULL IDENTITY(1,1), 
     id_carrito BIGINT , 
     fecha DATETIME , 
     sub_total BIGINT , 
     total BIGINT , 
     propina BIGINT , 
     id_usuario BIGINT 
    )
GO

ALTER TABLE Venta ADD CONSTRAINT Venta_PK PRIMARY KEY CLUSTERED (id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

ALTER TABLE Carrito_Cartas 
    ADD CONSTRAINT Carrito_Cartas_Carrito_FK FOREIGN KEY 
    ( 
     id_carrito
    ) 
    REFERENCES Carrito 
    ( 
     id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE Carrito_Cartas 
    ADD CONSTRAINT Carrito_Cartas_Cartas_FK FOREIGN KEY 
    ( 
     id_carta
    ) 
    REFERENCES Cartas 
    ( 
     id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE Carrito 
    ADD CONSTRAINT Carrito_Usuario_FK FOREIGN KEY 
    ( 
     id_usuario
    ) 
    REFERENCES Usuario 
    ( 
     id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE Carta_ingredientes 
    ADD CONSTRAINT Carta_ingredientes_Cartas_FK FOREIGN KEY 
    ( 
     id_carta
    ) 
    REFERENCES Cartas 
    ( 
     id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE Carta_ingredientes 
    ADD CONSTRAINT Carta_ingredientes_Ingredientes_FK FOREIGN KEY 
    ( 
     id_ingrediente
    ) 
    REFERENCES Ingredientes 
    ( 
     id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE Cartas 
    ADD CONSTRAINT Cartas_Fotos_FK FOREIGN KEY 
    ( 
     id_foto
    ) 
    REFERENCES Fotos 
    ( 
     id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE Cartas 
    ADD CONSTRAINT Cartas_Promociones_FK FOREIGN KEY 
    ( 
     id_promocion
    ) 
    REFERENCES Promociones 
    ( 
     id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE Delivery 
    ADD CONSTRAINT Delivery_Estados_FK FOREIGN KEY 
    ( 
     id_estado
    ) 
    REFERENCES Estados 
    ( 
     id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE Delivery 
    ADD CONSTRAINT Delivery_Repartidor_FK FOREIGN KEY 
    ( 
     id_repartidor
    ) 
    REFERENCES Repartidor 
    ( 
     id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE Delivery 
    ADD CONSTRAINT Delivery_Venta_FK FOREIGN KEY 
    ( 
     id_venta
    ) 
    REFERENCES Venta 
    ( 
     id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE Venta 
    ADD CONSTRAINT Venta_Carrito_FK FOREIGN KEY 
    ( 
     id_carrito
    ) 
    REFERENCES Carrito 
    ( 
     id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE Venta 
    ADD CONSTRAINT Venta_Usuario_FK FOREIGN KEY 
    ( 
     id_usuario
    ) 
    REFERENCES Usuario 
    ( 
     id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO


-- AGREGAR INGREDIENTES
INSERT INTO Ingredientes (nombre, descripcion)
VALUES ('Palta','Palta importada');

    INSERT INTO Ingredientes (nombre, descripcion)
    VALUES ('Salmon','Salmon fresco');

    INSERT INTO Ingredientes (nombre, descripcion)
    VALUES ('queso crema','Palta importada');

    INSERT INTO Ingredientes (nombre, descripcion)
    VALUES ('Camaron','Cola de camaron importado de china');

    INSERT INTO Ingredientes (nombre, descripcion)
    VALUES ('Pollo Teriyaki','Pollo cocido previamente adobado en salsa teriyaki');

    INSERT INTO Ingredientes (nombre, descripcion)
    VALUES ('Kanikama','carne de pescado blanco finamente picada y curada');

    INSERT INTO Ingredientes (nombre, descripcion)
    VALUES ('Arroz','Arroz blanco');

    INSERT INTO Ingredientes (nombre, descripcion)
    VALUES ('Carne Salteada','carne frita a fuego vivo');

    INSERT INTO Ingredientes (nombre, descripcion)
    VALUES ('Champinon','hongo de uso gastronomico');

    INSERT INTO Ingredientes (nombre, descripcion)
    VALUES ('Panko','Pan japones frito');

    INSERT INTO Ingredientes (nombre, descripcion)
    VALUES ('Sesamo','planta cultivada por sus semillas ricas en aceite');

    INSERT INTO Ingredientes (nombre, descripcion)
    VALUES ('Ciboulette','hierba con sabor calido y fresco tambien conocido como cebollin');

    INSERT INTO Ingredientes (nombre, descripcion)
    VALUES ('Nori','alga marina');

    INSERT INTO Ingredientes (nombre, descripcion)
    VALUES ('cerdo','cerdo al wok');

    INSERT INTO Ingredientes (nombre, descripcion)
    VALUES ('masago','Pez pequeno tambien conocido como capelan importado de china');


-- AGREGAR PROMOCION
INSERT INTO Promociones (nombre, descripcion, porc_descuento, max_descuento, created_at)
VALUES ('Promocion California Tori Chesse','1 y 16 de cada mes descuento en la compra de California Tori Chesse',20,560,'2020-07-02');

INSERT INTO Promociones (nombre, descripcion, porc_descuento, max_descuento, created_at)
VALUES ('Promocion California Ebi Chesse','2 y 17 de cada mes descuento en la compra de California Ebi Chesse',19,532,'2020-07-02');

INSERT INTO Promociones (nombre, descripcion, porc_descuento, max_descuento, created_at)
VALUES ('Promocion California Kani Chesse','3 y 18 de cada mes descuento en la compra de California Kani Chesse',15,420,'2020-07-02');

INSERT INTO Promociones (nombre, descripcion, porc_descuento, max_descuento, created_at)
VALUES ('Promocion California Sake','4 y 19 de cada mes descuento en la compra de California Sake',15,420,'2020-07-02');

INSERT INTO Promociones (nombre, descripcion, porc_descuento, max_descuento, created_at)
VALUES ('Promocion Ebimaki','5 y 20 de cada mes descuento en la compra de ebimaki',10,160,'2020-07-02');

INSERT INTO Promociones (nombre, descripcion, porc_descuento, max_descuento, created_at)
VALUES ('Promocion Tori Maki','6 y 21 de cada mes descuento en la compra de Tori Maki',10,160,'2020-07-02');

INSERT INTO Promociones (nombre, descripcion, porc_descuento, max_descuento, created_at)
VALUES ('Promocion Sake Maki','7 y 22 de cada mes descuento en la compra de Sake Maki',10,160,'2020-07-02');

INSERT INTO Promociones (nombre, descripcion, porc_descuento, max_descuento, created_at)
VALUES ('Promocion Kani Maki','8 y 23 de cada mes descuento en la compra de Kani Maki',10,160,'2020-07-02');

INSERT INTO Promociones (nombre, descripcion, porc_descuento, max_descuento, created_at)
VALUES ('Promocion Masago Cream','9 y 24 de cada mes descuento en la compra de Masago Cream',21,924,'2020-07-02');

INSERT INTO Promociones (nombre, descripcion, porc_descuento, max_descuento, created_at)
VALUES ('Promocion Edu cream','10 y 25 de cada mes descuento en la compra de Edu cream',20,900,'2020-07-02');

INSERT INTO Promociones (nombre, descripcion, porc_descuento, max_descuento, created_at)
VALUES ('Promocion Futebi','11 y 26 de cada mes descuento en la compra de Futebi',25,1100,'2020-07-02');

INSERT INTO Promociones (nombre, descripcion, porc_descuento, max_descuento, created_at)
VALUES ('Promocion Kasai','12 y 27 de cada mes descuento en la compra de Kasai',22,1034,'2020-07-02');

INSERT INTO Promociones (nombre, descripcion, porc_descuento, max_descuento, created_at)
VALUES ('Promocion Sumo','13 y 28 de cada mes descuento en la compra de Sumo',22,1034,'2020-07-02');

INSERT INTO Promociones (nombre, descripcion, porc_descuento, max_descuento, created_at)
VALUES ('Promocion Chin','14 y 29 de cada mes descuento en la compra de Chin',18,864,'2020-07-02');

INSERT INTO Promociones (nombre, descripcion, porc_descuento, max_descuento, created_at)
VALUES ('Promocion California Tempura','15 y 30 de cada mes descuento en la compra de california Tempura',19,722,'2020-07-02');

-- AGREGAR ESTADOS
INSERT INTO Estados (nombre, created_at)
VALUES ('FRIO', '2020-07-02');

INSERT INTO Estados (nombre, created_at)
VALUES ('FRITOS', '2020-07-02');

--  AGREGAR REPARTIDOR
INSERT INTO Repartidor (nombre, apellidos, celular, rut, dv, email)
VALUES ('Jose','Perez','987321161','15184033','4','jose.perez@gmail.com');

INSERT INTO Repartidor (nombre, apellidos, celular, rut, dv, email)
VALUES ('Pedro','Rodriguez','967273974','18567897','5','pedro.rodriguez.mx@gmail.com');

INSERT INTO Repartidor (nombre, apellidos, celular, rut, dv, email)
VALUES ('Jorge','Lopez','999436348','13737808','5','jorge.lopez.28@gmail.com');