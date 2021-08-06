--Comparación entre los distintos M&M: cual factura más/menos por servicios

SELECT fr.rif_franquicia, fr.nombre_f, os.num_ficha,
        SUM ( fa.descuento_f * ((os.cant_producto * os.precio_prod) + os.precio_mano_obra) ) AS total_factura
INTO TEMP R1
FROM ordenes_servicio os, registros r, facturas fa, franquicias fr
WHERE os.num_ficha = r.num_ficha
AND r.rif_franquicia = fr.rif_franquicia
AND fa.num_factura = (SELECT faS.num_factura
                        FROM facturas_servicio faS
                        WHERE faS.num_ficha = os.num_ficha)
GROUP BY 1, 2, 3;

SELECT rif_franquicia, nombre_f, SUM (total_factura) AS total_franquicia
FROM R1
GROUP BY 1, 2
ORDER BY 1 ASC;
