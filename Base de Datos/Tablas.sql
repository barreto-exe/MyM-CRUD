---------------------- Dominios ----------------------
CREATE DOMAIN dom_codigo AS VARCHAR(15);    --- dom para todos los códigos ---

CREATE DOMAIN dom_nombre AS VARCHAR(15);    --- dom para todos los nombres ---

CREATE DOMAIN dom_rif AS VARCHAR(10);       --- dom para todos los rifs ---

CREATE DOMAIN dom_cedula AS VARCHAR(10);       --- dom para todas las cédulas---

CREATE DOMAIN dom_direccion AS VARCHAR(75);   --- dom para las direcciones ---

CREATE DOMAIN dom_telefono AS VARCHAR(12);   --- dom para los teléfonos ---

CREATE DOMAIN dom_fecha AS DATE;             --- dom para todas las fechas ---

CREATE DOMAIN dom_monto AS REAL;             --- dom para todos los precios / sueldos / etc ---

CREATE DOMAIN dom_email AS VARCHAR(50);             --- dom para todos los emails ---

CREATE DOMAIN dom_clave AS VARCHAR(16);             --- dom para todos las contraseñas ---
 
---------------------- Tablas ----------------------
CREATE TABLE "ciudades"(
 	nom_ciudad  		dom_nombre  	NOT NULL,
 	PRIMARY KEY (nom_ciudad) 
);

CREATE TABLE "franquicias"(
 	rif_franquicia  	dom_rif  		NOT NULL,
 	nombre_f 	    	dom_nombre  	NOT NULL,
 	direccion_f     	dom_direccion 	NOT NULL,
 	ced_encargado  		dom_cedula   	NOT NULL,
 	nom_ciudad  		dom_nombre   	NOT NULL,
	clave			dom_clave		NOT NULL,
 	PRIMARY KEY (rif_franquicia), 
 	FOREIGN KEY (nom_ciudad) REFERENCES "ciudades"
  		ON DELETE RESTRICT
  		ON UPDATE CASCADE 
);

CREATE TABLE "personas"(
	cedula_p      		dom_cedula  	NOT NULL,
 	nombre_p  			dom_nombre  	NOT NULL,
 	telefono_p  		dom_telefono	NOT NULL,
 	tipo_p  				CHAR(1)   		NOT NULL,
 	CONSTRAINT status_cliente
  		CHECK  (tipo_p = 'E' OR     --- empleado ---
      			tipo_p = 'C'),       --- cliente ---
 	PRIMARY KEY (cedula_p) 
);

CREATE TABLE "empleados"(
 	ced_empleado  		dom_cedula  	NOT NULL,
 	sueldo  	    	dom_monto		NOT NULL,
 	direccion_e     	dom_direccion 	NOT NULL,
	es_encargado		BOOLEAN			NOT NULL,
 	rif_franquicia 	    dom_rif,
 	PRIMARY KEY (ced_empleado), 
 	FOREIGN KEY (ced_empleado) REFERENCES "personas"
  		ON DELETE CASCADE
  		ON UPDATE CASCADE,
 	FOREIGN KEY (rif_franquicia) REFERENCES "franquicias"
  		ON DELETE CASCADE
  		ON UPDATE CASCADE
);

CREATE TABLE "encargados"(
 	ced_encargado  	dom_cedula  	NOT NULL,
 	fecha_incio 	dom_fecha  		NOT NULL,
 	PRIMARY KEY (ced_encargado), 
 	FOREIGN KEY (ced_encargado) REFERENCES "empleados"
  		ON DELETE CASCADE
  		ON UPDATE CASCADE
);

CREATE TABLE "clientes"(
 	ced_cliente  	dom_cedula  	NOT NULL,
 	telefono_alt 	dom_telefono  	NOT NULL,
 	email  	    	dom_email		NOT NULL,
 	PRIMARY KEY (ced_cliente), 
 	FOREIGN KEY (ced_cliente) REFERENCES "personas"
  		ON DELETE CASCADE
  		ON UPDATE CASCADE
);
---------------------- Claves Foráneas ----------------------
ALTER TABLE "franquicias" 
  ADD FOREIGN KEY ("ced_encargado") REFERENCES "encargados" ("ced_encargado") 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE;

---------------------- Índices ----------------------
CREATE INDEX localizacion ON "franquicias" (nom_ciudad);