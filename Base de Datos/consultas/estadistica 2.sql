-- Personal que realiza más/menos servicios por mes (en esta franquicia).

SELECT os.numficha, os.codServicio, os.empleado
INTO TEMP R1
FROM ordenes_servicio os, registros r
WHERE os.numFicha = r.numFicha
AND r.franquicia = ?
AND to_char(r.fechaEntrada, 'yyyy') = ?     -- año
AND to_char(r.fechaEntrada, 'mm') = ?       -- mes
GROUP BY 1, 2;

SELECT R1.empleado, COUNT(R1.codServicio)
FROM R1
GROUP BY 1;