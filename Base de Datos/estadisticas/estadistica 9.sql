-- Proveedor que nos suministra más/menos productos

SELECT p.rif_proveedor, p.razon_Social, COUNT(d.cod_producto) AS cantidad
INTO TEMP R1
FROM distribuyen d, proveedores p
WHERE d.rif_proveedor = p.rif_proveedor
GROUP BY 1, 2;

SELECT rif_proveedor, razon_Social
INTO TEMP proveedor_menos_suministra_T
FROM R1
WHERE cantidad = (SELECT MIN(cantidad)
                    FROM R1);

SELECT rif_proveedor, razon_Social
INTO TEMP proveedor_mas_suministra_T
FROM R1
WHERE cantidad = (SELECT MAX(cantidad)
                    FROM R1);

SELECT  menos.rif_proveedor AS  rif_proveedor_que_menos_suministra,
        menos.razon_Social   AS  razon_social_proveedor_que_menos_suministra,
        mas.rif_proveedor   AS  rif_proveedor_que_mas_suministra,
        mas.razon_social    AS  razon_social_proveedor_que_mas_suministra
FROM proveedor_menos_suministra_T menos, proveedor_mas_suministra_T mas;
