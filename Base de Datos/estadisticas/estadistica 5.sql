-- Servicio más/menos solicitado (en esta franquicia).

SELECT os.num_Ficha, s.nombre_S
INTO TEMP R1
FROM ordenes_servicio os, servicios s, registros r
WHERE os.cod_Servicio = s.cod_Servicio
AND os.num_Ficha = r.num_Ficha
AND r.franquicia = ?
GROUP BY 1, 2;

SELECT nombre_S, COUNT(*) AS cantidad
INTO TEMP R2
FROM R1
GROUP BY 1;

SELECT nombre_S
INTO servicio_menos_solicitado_T
FROM R2
WHERE cantidad = (SELECT MIN(cantidad)
                    FROM R2);

SELECT nombre_S
INTO servicio_mas_solicitado_T
FROM R2
WHERE cantidad = (SELECT MAX(cantidad)
                    FROM R2);

SELECT  menos.nombre_S   AS  servicio_menos_solicitado,
        mas.nombre_S     AS  servicio_mas_solicitado
FROM servicio_menos_solicitado_T menos, servicio_mas_solicitado_T mas;
