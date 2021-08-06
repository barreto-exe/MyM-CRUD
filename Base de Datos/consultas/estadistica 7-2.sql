--Comparación entre los distintos M&M: cual factura más/menos por ventas

SELECT fr.rif_franquicia, fr.nombre_f, r.factTienda, 
        SUM ( fa.descuento_f * p.precio * r.cant_comprada) AS total_factura
INTO TEMP R1
FROM registran r, franquicias fr, facturas fa, productos p
WHERE r.num_fact_tienda = fa.num_factura
AND fa.franquicia = fr.rif_franquicia
AND r.cod_tienda_prod = p.cod_producto
GROUP BY 1, 2, 3;

SELECT rif_franquicia, nombre_f, SUM (total_factura) AS total_franquicia
FROM R1
GROUP BY 1, 2;
