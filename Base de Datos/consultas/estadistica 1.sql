-- Marca de vehículos que más atendemos por tipo de servicio

SELECT s.nombre_S, ma.nombre_Marca, COUNT(r.*) AS cantidad
INTO TEMP R1
FROM registro r, ordenes_servicio os, servicios s
     vehiculos v, modelo mo, marca ma
WHERE r.placa_Vehiculo = v.placa_Vehiculo
AND v.modelo = mo.nombre_Mo
AND mo.marca = ma.cod_Marca
AND r.num_Ficha = os.num_Ficha
AND os.cod_Servicio = s.cod_Servicio
AND r.franquicia = ?
GROUP BY 1, 2;

SELECT R1.nombre_S, MAX(R1.cantidad) AS maximo
INTO TEMP R2
FROM R1
GROUP BY 1;

SELECT R1.*
FROM R1, R2
WHERE R1.nombre_S = R2.nombre_S
AND R1.cantidad = R2.maximo
ORDER BY R1.nombre_s;