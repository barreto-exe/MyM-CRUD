-- Personal que realiza más/menos servicios por mes (en esta franquicia).

SELECT os.num_ficha, os.cod_Servicio, os.ced_empleado
INTO TEMP R1
FROM ordenes_servicio os, registros r
WHERE os.num_Ficha = r.num_Ficha
AND r.franquicia = ?
AND to_char(r.fecha_Ent, 'yyyy') = ?     -- año
AND to_char(r.fecha_Ent, 'mm') = ?       -- mes
GROUP BY 1, 2, 3;

SELECT R1.ced_empleado, COUNT(R1.cod_Servicio)
FROM R1
GROUP BY 1;