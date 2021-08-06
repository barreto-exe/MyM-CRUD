-- Proveedor que nos suministra más/menos productos

SELECT p.rif_proveedor, p.razonSocial, COUNT(d.producto) AS cantidad
INTO TEMP R1
FROM distribuyen d, proveedor p
WHERE d.proveedor = p.rif_proveedor
GROUP BY 1, 2;

SELECT rif_proveedor, razonSocial
FROM proveedor_menos_suministra_T
WHERE cantidad = (SELECT MIN(cantidad)
                    FROM R1);

SELECT rif_proveedor, razonSocial
FROM proveedor_mas_suministra_T
WHERE cantidad = (SELECT MAX(cantidad)
                    FROM R1);

SELECT  menos.rif_proveedor AS  rif_proveedor_que_menos_suministra
        menos.razonSocial   AS  razon_social_proveedor_que_menos_suministra
        mas.rif_proveedor   AS  rif_proveedor_que_mas_suministra
        mas.razon_social    AS  razon_social_proveedor_que_mas_suministra
FROM proveedor_menos_suministra_T menos, proveedor_mas_suministra_T mas;
