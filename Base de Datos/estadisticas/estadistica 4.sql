-- Producto con mayor/menos salida por ventas (en esta franquicia)

SELECT p.nombre_p, SUM(r.cant_Comprada) AS suma
INTO TEMP R1
FROM registran r, productos p, facturas fa
WHERE fa.franquicia = ?
WHERE fa.num_Factura = r.num_fact_tienda
AND r.cod_tienda_prod = p.cod_Producto
GROUP BY 1;

SELECT nombre_p
INTO TEMP producto_menos_vendido_T
FROM R1
WHERE suma = (SELECT MIN(suma)
                FROM R1);

SELECT nombre_p
INTO TEMP producto_mas_vendido_T
FROM R1
WHERE suma = (SELECT MAX(suma)
                FROM R1);

SELECT  menosv.nombre_p   AS producto_menos_vendido,
        masv.nombre_p     AS producto_mas_vendido
FROM producto_menos_vendido_T menosv, producto_mas_vendido_T masv;