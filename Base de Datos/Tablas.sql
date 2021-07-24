---------------------- Dominios ----------------------
CREATE DOMAIN dom_codigo AS VARCHAR(15);    --- dom para todos los códigos ---

CREATE DOMAIN dom_nombre AS VARCHAR(15);    --- dom para todos los nombres ---

CREATE DOMAIN dom_rif AS VARCHAR(10);       --- dom para todos los rifs ---

CREATE DOMAIN dom_cedula AS VARCHAR(10);       --- dom para todas las cédulas---

CREATE DOMAIN dom_direccion AS VARCHAR(75);   --- dom para las direcciones ---

CREATE DOMAIN dom_descripcion AS VARCHAR(75);   --- dom para las descripciones ---

CREATE DOMAIN dom_telefono AS VARCHAR(12);   --- dom para los teléfonos ---

CREATE DOMAIN dom_fecha AS DATE;             --- dom para todas las fechas ---

CREATE DOMAIN dom_monto AS REAL;             --- dom para todos los precios / sueldos / etc ---

CREATE DOMAIN dom_email AS VARCHAR(50);             --- dom para todos los emails ---

CREATE DOMAIN dom_clave AS VARCHAR(16);             --- dom para todos las contraseñas ---

CREATE DOMAIN dom_cantidad AS INTEGER;             --- dom para las cantidades ---
 
---------------------- Tablas ----------------------
CREATE TABLE servicios(
 	cod_servicio  		dom_codigo  		NOT NULL,
	nombre_s			dom_nombre			NOT NULL,
	descripcion_s		dom_descripcion		NOT NULL,	
	tiene_reserva		BOOLEAN				NOT NULL,
	tiempo_r			dom_cantidad,			 
 	PRIMARY KEY (cod_servicio) 
);

CREATE TABLE reservas(
	num_reserva      		dom_codigo  	NOT NULL,
 	fecha_emision  			dom_fecha  		NOT NULL,
	abono					dom_monto		NOT NULL,
	ced_cliente				dom_cedula		NOT NULL,
 	PRIMARY KEY (num_reserva)
);

CREATE TABLE registros(
	num_ficha      		dom_codigo  	NOT NULL,
 	fecha_ent  			dom_fecha	  	NOT NULL,
	fecha_sal_est		dom_fecha		NOT NULL,
	fecha_sal_real		dom_fecha		NOT NULL,
	nom_autorizado		dom_nombre		NOT NULL,
	ced_autorizado		dom_cedula		NOT NULL,		
	placa_vehiculo		dom_codigo		NOT NULL,
	rif_franquicia		dom_rif			NOT NULL,
	num_reserva			dom_codigo,
 	PRIMARY KEY (num_ficha)
);

CREATE TABLE franquicias(
 	rif_franquicia  	dom_rif  		NOT NULL,
 	nombre_f 	    	dom_nombre  	NOT NULL,
 	ciudad  			dom_nombre   	NOT NULL,
 	direccion_f     	dom_direccion 	NOT NULL,
 	supervisor  		dom_cedula   	NOT NULL,
	fecha_incio_sup		dom_fecha		NOT NULL,
	clave				dom_clave		NOT NULL,
 	PRIMARY KEY (rif_franquicia)
);

CREATE TABLE empleados(
	cedula_e      		dom_cedula  	NOT NULL,
 	nombre_e  			dom_nombre  	NOT NULL,
 	telefono_e  		dom_telefono	NOT NULL,
 	sueldo  	    	dom_monto		NOT NULL,
 	direccion_e     	dom_direccion 	NOT NULL,
 	rif_franquicia 	    dom_rif			NOT NULL,
 	PRIMARY KEY (cedula_e)
);

CREATE TABLE clientes(
	cedula_c      		dom_cedula  	NOT NULL,
 	nombre_c  			dom_nombre  	NOT NULL,
 	tlf_principal  		dom_telefono	NOT NULL,
 	tlf_alternativo 	dom_telefono  	NOT NULL,
 	email  	    		dom_email		NOT NULL,
 	PRIMARY KEY (cedula_c)
);

CREATE TABLE vehiculos(
	placa				dom_codigo		NOT NULL,
	cod_vehiculo   		dom_codigo  	NOT NULL,
 	tipo_aceite			dom_nombre  	NOT NULL,
	fecha_adq			dom_fecha		NOT NULL,
	nom_mecanico		dom_nombre		NOT NULL,
	tlf_mecanico		dom_telefono	NOT NULL,
	ced_dueno			dom_cedula		NOT NULL,
	nom_modelo			dom_nombre		NOT NULL,
 	PRIMARY KEY (placa)
);

CREATE TABLE modelos(
	nom_modelo			dom_nombre		NOT NULL,
	aceite_motor		dom_descripcion	NOT NULL,
	aceite_caja			dom_descripcion	NOT NULL,
	peso		   		dom_monto	  	NOT NULL,
 	num_puestos			dom_cantidad  	NOT NULL,
	refrigerante		dom_nombre		NOT NULL,
	gasolina			dom_nombre		NOT NULL,
	cod_marca			dom_codigo		NOT NULL,
	cod_tipo_vehiculo	dom_codigo		NOT NULL,
 	PRIMARY KEY (nom_modelo)
);

CREATE TABLE marcas(
	cod_marca      		dom_codigo  	NOT NULL,
 	nom_marca  			dom_nombre  	NOT NULL,
	descripcion_ma		dom_descripcion	NOT NULL,
 	PRIMARY KEY (cod_marca)
);

CREATE TABLE tipos_vehiculo(
	cod_tipo_vehiculo	dom_codigo  	NOT NULL,
	descripcion_v		dom_descripcion	NOT NULL,
 	PRIMARY KEY (cod_tipo_vehiculo)
);

CREATE TABLE bancos(
	cod_banco      		dom_codigo  	NOT NULL,
 	nombre_b  			dom_nombre  	NOT NULL,
 	PRIMARY KEY (cod_banco)
);

CREATE TABLE modalidades_pago(
	descripcion_m      		dom_descripcion  	NOT NULL,
 	PRIMARY KEY (descripcion_m)
);

CREATE TABLE lineas_suministro(
	cod_linea      		dom_codigo  	NOT NULL,
 	nombre_l  			dom_nombre  	NOT NULL,
 	PRIMARY KEY (cod_linea)
);

CREATE TABLE fabricantes(
	cod_fabricante     		dom_codigo  	NOT NULL,
 	nombre_f	  			dom_nombre  	NOT NULL,
 	PRIMARY KEY (cod_fabricante)
);

CREATE TABLE proveedores(
	rif_proveedor  			dom_rif  		NOT NULL,
 	razon_social  	    	dom_nombre		NOT NULL,
	direccion_p				dom_direccion	NOT NULL,
 	tlf_local  				dom_telefono	NOT NULL,
 	tlf_celular		 		dom_telefono  	NOT NULL,
 	nom_contacto			dom_nombre  	NOT NULL,
 	PRIMARY KEY (rif_proveedor)
);
---------------------- Claves Foráneas ----------------------
ALTER TABLE reservas 
  ADD FOREIGN KEY (ced_cliente) REFERENCES clientes (cedula_c) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE;

ALTER TABLE registros
  ADD FOREIGN KEY (placa_vehiculo) REFERENCES vehiculos (placa) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE,
  ADD FOREIGN KEY (rif_franquicia) REFERENCES franquicias (rif_franquicia) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE,
  ADD FOREIGN KEY (num_reserva) REFERENCES reservas (num_reserva) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE;

ALTER TABLE franquicias 
  ADD FOREIGN KEY (supervisor) REFERENCES empleados (cedula_e) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE;

ALTER TABLE empleados 
  ADD FOREIGN KEY (rif_franquicia) REFERENCES franquicias (rif_franquicia) 
	ON DELETE CASCADE 
	ON UPDATE CASCADE;

ALTER TABLE vehiculos
  ADD FOREIGN KEY (ced_dueno) REFERENCES clientes (cedula_c) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE,
  ADD FOREIGN KEY (nom_modelo) REFERENCES modelos (nom_modelo) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE;

ALTER TABLE modelos
  ADD FOREIGN KEY (cod_marca) REFERENCES marcas (cod_marca) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE,
  ADD FOREIGN KEY (cod_tipo_vehiculo) REFERENCES tipos_vehiculo (cod_tipo_vehiculo) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE;
---------------------- Índices ----------------------