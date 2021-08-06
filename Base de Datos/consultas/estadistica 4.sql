-- Producto con mayor/menos salida por ventas (en esta franquicia)

SELECT p.nombre, SUM(r.cantComprada) AS suma
INTO TEMP R1
FROM registran r, productos p, facturas fa
WHERE fa.franquicia = ?
AND fa.num_Factura = r.fact_Tienda
AND r.tienda_Producto = p.cod_Producto
GROUP BY 1;

SELECT nombre
INTO TEMP producto_menos_vendido_T
FROM R1
WHERE suma = (SELECT MIN(suma)
                FROM R1);

SELECT nombre
FROM TEMP producto_mas_vendido_T
WHERE suma = (SELECT MAX(suma)
                FROM R1);

SELECT  menosv.nombre   AS producto_menos_vendido
        masv.nombre     AS producto_mas_vendido
FROM producto_menos_vendido_T menosv, producto_mas_vendido_T masv;