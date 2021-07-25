---------------------- Dominios ----------------------
CREATE DOMAIN dom_codigo AS VARCHAR(15);    --- dom para todos los códigos ---

CREATE DOMAIN dom_numero AS VARCHAR(20);    --- dom para todos los números ---

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
	num_reserva      		dom_numero  	NOT NULL,
 	fecha_emision  			dom_fecha  		NOT NULL,
	abono					dom_monto		NOT NULL,
	ced_cliente				dom_cedula		NOT NULL,
 	PRIMARY KEY (num_reserva)
);

CREATE TABLE registros(
	num_ficha      		dom_numero  	NOT NULL,
 	fecha_ent  			dom_fecha	  	NOT NULL,
	fecha_sal_est		dom_fecha		NOT NULL,
	fecha_sal_real		dom_fecha		NOT NULL,
	nom_autorizado		dom_nombre		NOT NULL,
	ced_autorizado		dom_cedula		NOT NULL,		
	placa_vehiculo		dom_codigo		NOT NULL,
	rif_franquicia		dom_rif			NOT NULL,
	num_reserva			dom_numero,
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

CREATE TABLE ordenes_compra(
	cod_orden_c  				dom_codigo 		NOT NULL,
 	fecha  	    			dom_fecha		NOT NULL,
	rif_franquicia			dom_rif			NOT NULL,
 	rif_proveedor  			dom_rif			NOT NULL,
 	PRIMARY KEY (cod_orden)
);

CREATE TABLE facturas_proveedores(
	num_factura  			dom_numero 		NOT NULL,
 	fecha_pago  	    	dom_fecha		NOT NULL,
	monto					dom_monto		NOT NULL,
 	cod_orden_compra  		dom_codigo		NOT NULL,
 	PRIMARY KEY (num_factura)
);

CREATE TABLE productos(
	cod_producto  		dom_codigo 			NOT NULL,
 	precio_p  	    	dom_monto			NOT NULL,
	nombre_p			dom_nombre			NOT NULL,
 	descripcion_p  		dom_descripcion		NOT NULL,
	tipo_p				CHAR(1)				NOT NULL,
	cod_fabricante		dom_codigo			NOT NULL,
 	PRIMARY KEY (cod_producto)
);

CREATE TABLE tienda_productos(
	cod_producto  		dom_codigo 			NOT NULL,
 	PRIMARY KEY (cod_producto)
);

CREATE TABLE eco_productos(
	cod_producto  		dom_codigo 			NOT NULL,
	es_ecologico  		BOOLEAN 			NOT NULL,
	cod_linea	  		dom_codigo 			NOT NULL,
 	PRIMARY KEY (cod_producto)
);

CREATE TABLE facturas(
	num_factura  		dom_numero 			NOT NULL,
	fecha_emision  		dom_fecha 			NOT NULL,
	descuento_f	  		dom_monto 			NOT NULL,
	tipo_fact			CHAR(1)				NOT NULL,
	ced_cliente			dom_cedula			NOT NULL,
 	PRIMARY KEY (num_factura)
);

CREATE TABLE facturas_servicio(
	num_factura  		dom_numero 			NOT NULL,
	num_ficha			dom_numero			NOT NULL,
 	PRIMARY KEY (num_factura)
);

CREATE TABLE facturas_tienda(
	num_factura  		dom_numero 			NOT NULL,
	modalidad_p			dom_descripcion		NOT NULL,
 	PRIMARY KEY (num_factura)
);

CREATE TABLE actividades(
	cod_servicio  		dom_codigo 			NOT NULL,
	num_actividad		dom_numero			NOT NULL,
	monto				dom_monto			NOT NULL,
	descripcion_a		dom_descripcion		NOT NULL,
 	PRIMARY KEY (cod_servicio, num_actividad)
);

CREATE TABLE ordenes_servicio(
	num_ficha  			dom_numero 			NOT NULL,
	num_orden_s			dom_numero			NOT NULL,
	cant_producto		dom_cantidad		NOT NULL,
	precio_prod			dom_monto			NOT NULL,
	precio_mano_obra	dom_monto			NOT NULL,
	cod_servicio		dom_codigo			NOT NULL,
	num_actividad		dom_numero			NOT NULL,
	cod_producto		dom_codigo			NOT NULL,
	ced_empleado		dom_cedula			NOT NULL,
 	PRIMARY KEY (num_ficha, num_orden_s)
);

CREATE TABLE mantenimientos(
	nom_modelo  		dom_nombre 			NOT NULL,
	nom_mantenimiento	dom_nombre			NOT NULL,
	tiempo_uso			dom_cantidad		NOT NULL,
	kilometraje			dom_monto			NOT NULL,
 	PRIMARY KEY (nom_modelo, nom_mantenimiento)
);

CREATE TABLE pagos(
	num_factura  		dom_numero 			NOT NULL,
	num_pago			dom_numero			NOT NULL,
	monto				dom_cantidad		NOT NULL,
	fecha_pago			dom_monto			NOT NULL,
	modalidad_p			dom_codigo			NOT NULL,
	cod_banco			dom_codigo,
	num_tarjeta			dom_numero,
 	PRIMARY KEY (num_factura, num_pago)
);

CREATE TABLE inventarios(
	rif_franquicia  		dom_rif 			NOT NULL,
	num_inventario			dom_numero			NOT NULL,
	cod_producto			dom_codigo			NOT NULL,
	cant_min				dom_cantidad		NOT NULL,
	cant_max				dom_cantidad		NOT NULL,
	cant_teorica			dom_cantidad		NOT NULL,
	cant_fisica				dom_cantidad		NOT NULL,
	fecha_fisi				dom_fecha			NOT NULL,
 	PRIMARY KEY (rif_franquicia, num_inventario)
);

CREATE TABLE ajustes(
	rif_franquicia			dom_rif				NOT NULL,
	num_inventario			dom_numero			NOT NULL,
	fecha					dom_fecha			NOT NULL,
	cantidad				dom_cantidad		NOT NULL,
	tipo_ajuste				CHAR(1)				NOT NULL,							
	PRIMARY KEY (rif_franquicia, num_inventario, fecha)
);

CREATE TABLE apartan(
	cod_servicio			dom_codigo			NOT NULL,
	num_actividad			dom_numero			NOT NULL,
	num_reserva				dom_numero			NOT NULL,
	fecha					dom_fecha			NOT NULL,						
	PRIMARY KEY (cod_servicio, num_actividad, num_reserva)
);

CREATE TABLE ofrecen(
	rif_franquicia			dom_rif				NOT NULL,
	cod_servicio			dom_codigo			NOT NULL,
	capacidad				dom_cantidad		NOT NULL,					
	PRIMARY KEY (rif_franquicia, cod_servicio)
);

CREATE TABLE reciben(
	rif_franquicia			dom_rif				NOT NULL,
	nom_modelo				dom_nombre			NOT NULL,				
	PRIMARY KEY (rif_franquicia, nom_modelo)
);

CREATE TABLE utilizan(
	nom_modelo				dom_nombre			NOT NULL,
	cod_linea				dom_codigo			NOT NULL,	
	cant_estimada			dom_cantidad		NOT NULL,			
	PRIMARY KEY (nom_modelo, cod_linea)
);

CREATE TABLE registran(
	num_fact_tienda			dom_numero			NOT NULL,
	cod_tienda_prod			dom_codigo			NOT NULL,	
	cant_comprada			dom_cantidad		NOT NULL,			
	PRIMARY KEY (num_fact_tienda, cod_tienda_prod)
);

CREATE TABLE solicitan(
	cod_orden_compra		dom_codigo			NOT NULL,
	cod_producto			dom_codigo			NOT NULL,	
	cantidad				dom_cantidad		NOT NULL,			
	PRIMARY KEY (cod_orden_compra, cod_producto)
);

CREATE TABLE distribuyen(
	rif_proveedor			dom_rif				NOT NULL,
	cod_producto			dom_codigo			NOT NULL,			
	PRIMARY KEY (rif_proveedor, cod_producto)
);

CREATE TABLE mantenimientos_v(
	cod_vehiculo			dom_codigo			NOT NULL,
	fecha					dom_fecha			NOT NULL,	
	desc_mantenimiento		dom_descripcion		NOT NULL,			
	PRIMARY KEY (cod_vehiculo, fecha, desc_mantenimiento)
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

ALTER TABLE ordenes_compra
  ADD FOREIGN KEY (rif_franquicia) REFERENCES franquicias (rif_franquicia) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE,
  ADD FOREIGN KEY (rif_proveedor) REFERENCES proveedores (rif_proveedor) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE;

ALTER TABLE facturas_proveedores
  ADD FOREIGN KEY (cod_orden_compra) REFERENCES ordenes_compra (cod_orden_c) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE;

ALTER TABLE productos
  ADD FOREIGN KEY (cod_fabricante) REFERENCES fabricantes (cod_fabricante) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE;

ALTER TABLE tienda_productos
  ADD FOREIGN KEY (cod_producto) REFERENCES productos (cod_producto) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE;

ALTER TABLE eco_productos
  ADD FOREIGN KEY (cod_producto) REFERENCES productos (cod_producto) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE,
  ADD FOREIGN KEY (cod_linea) REFERENCES lineas_suministro (cod_linea) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE;

ALTER TABLE facturas
  ADD FOREIGN KEY (ced_cliente) REFERENCES clientes (cedula_c) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE;

ALTER TABLE facturas_servicio
  ADD FOREIGN KEY (num_factura) REFERENCES facturas (num_factura) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE,
  ADD FOREIGN KEY (num_ficha) REFERENCES registros (num_ficha) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE;

ALTER TABLE facturas_tienda
  ADD FOREIGN KEY (num_factura) REFERENCES facturas (num_factura) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE,
  ADD FOREIGN KEY (modalidad_p) REFERENCES modalidades_pago (descripcion_m) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE;

ALTER TABLE actividades
  ADD FOREIGN KEY (cod_servicio) REFERENCES servicios (cod_servicio) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE;

ALTER TABLE ordenes_servicio
  ADD FOREIGN KEY (num_ficha) REFERENCES registros (num_ficha) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE,
  ADD FOREIGN KEY (cod_servicio, num_actividad) REFERENCES actividades (cod_servicio, num_actividad) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE,
  ADD FOREIGN KEY (cod_producto) REFERENCES eco_productos (cod_producto) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE,
  ADD FOREIGN KEY (ced_empleado) REFERENCES empleados (cedula_e) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE;

ALTER TABLE mantenimientos
  ADD FOREIGN KEY (nom_modelo) REFERENCES modelos (nom_modelo) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE;

ALTER TABLE pagos
  ADD FOREIGN KEY (num_factura) REFERENCES facturas_servicio (num_factura) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE,
  ADD FOREIGN KEY (modalidad_p) REFERENCES modalidades_pago (descripcion_m) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE,
  ADD FOREIGN KEY (cod_banco) REFERENCES bancos (cod_banco) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE;

ALTER TABLE inventarios
  ADD FOREIGN KEY (rif_franquicia) REFERENCES franquicias (rif_franquicia) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE,
  ADD FOREIGN KEY (cod_producto) REFERENCES productos (cod_producto) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE;

ALTER TABLE ajustes
  ADD FOREIGN KEY (rif_franquicia, num_inventario) REFERENCES inventarios (rif_franquicia, num_inventario) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE;

ALTER TABLE apartan
  ADD FOREIGN KEY (cod_servicio, num_actividad) REFERENCES actividades (cod_servicio, num_actividad) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE,
  ADD FOREIGN KEY (num_reserva) REFERENCES reservas (num_reserva) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE;

ALTER TABLE ofrecen
  ADD FOREIGN KEY (rif_franquicia) REFERENCES franquicias (rif_franquicia) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE,
  ADD FOREIGN KEY (cod_servicio) REFERENCES servicios (cod_servicio) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE;

ALTER TABLE reciben
  ADD FOREIGN KEY (rif_franquicia) REFERENCES franquicias (rif_franquicia) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE,
  ADD FOREIGN KEY (nom_modelo) REFERENCES modelos (nom_modelo) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE;

ALTER TABLE utilizan
  ADD FOREIGN KEY (nom_modelo) REFERENCES modelos (nom_modelo) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE,
  ADD FOREIGN KEY (cod_linea) REFERENCES lineas_suministro (cod_linea) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE;

ALTER TABLE registran
  ADD FOREIGN KEY (num_fact_tienda) REFERENCES facturas_tienda (num_factura) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE,
  ADD FOREIGN KEY (cod_producto) REFERENCES tienda_productos (cod_producto) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE;

ALTER TABLE solicitan
  ADD FOREIGN KEY (cod_orden_compra) REFERENCES ordenes_compra (cod_orden_c) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE,
  ADD FOREIGN KEY (cod_producto) REFERENCES productos (cod_producto) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE;

ALTER TABLE distribuyen
  ADD FOREIGN KEY (rif_proveedor) REFERENCES proveedores (rif_proveedor) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE,
  ADD FOREIGN KEY (cod_producto) REFERENCES productos (cod_producto) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE;

ALTER TABLE mantenimientos_v
  ADD FOREIGN KEY (cod_vehiculo) REFERENCES vehiculos (cod_vehiculo) 
	ON DELETE RESTRICT 
	ON UPDATE CASCADE;
---------------------- Índices ----------------------