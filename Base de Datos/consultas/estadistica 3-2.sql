-- Cliente más / menos frecuente (en esta franquicia).

--Cliente menos frecuente
SELECT c.cedulaC, c.nombreC, COUNT(r.*) AS cantidad
INTO TEMP R1
FROM registro r, vehiculos v, clientes c
WHERE r.placaVehiculo = v.placa
AND v.dueno = c.cedulaC
AND r.franquicia = ?
GROUP BY 1, 2;

SELECT cedulaC, nombreC
FROM R1
WHERE cantidad = (SELECT MIN(cantidad)
                    FROM R1);