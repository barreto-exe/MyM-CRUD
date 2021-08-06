-- Marca de vehículos que más atendemos por tipo de servicio

SELECT s.nombreS, ma.nombreMarca, COUNT(r.*) AS cantidad
INTO TEMP R1
FROM registro r, ordenes_servicio os, servicios servicios
     vehiculos v, modelo mo, marca ma
WHERE r.placaVehiculo = v.placaVehiculo
AND v.modelo = mo.nombreMo
AND mo.marca = ma.codMarca
AND r.numFicha = os.numFicha
AND os.codServicio = s.codServicio
AND r.franquicia = ?
GROUP BY 1, 2;

SELECT R1.nombreS, MAX(R1.cantidad) AS maximo
INTO TEMP R2
FROM R1
GROUP BY 1;

SELECT R1.*
FROM R1, R2
WHERE R1.nombreS = R2.nombreS
AND R1.cantidad = R2.maximo;