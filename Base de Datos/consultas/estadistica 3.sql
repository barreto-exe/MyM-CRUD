-- Cliente más / menos frecuente (en esta franquicia).

SELECT c.cedula_C, c.nombre_C, COUNT(r.*) AS cantidad
INTO TEMP R1
FROM registro r, vehiculos v, clientes c
WHERE r.placa_Vehiculo = v.placa
AND v.dueno = c.cedula_C
AND r.franquicia = ?
GROUP BY 1, 2;

SELECT cedula_C, nombre_C
INTO TEMP menos_frecuente_T
FROM R1
WHERE cantidad = (SELECT MIN(cantidad)
                    FROM R1);

SELECT cedula_C, nombre_C
INTO TEMP mas_frecuente_T
FROM R1
WHERE cantidad = (SELECT MAX(cantidad)
                    FROM R1);

SELECT menosf.cedula_C   AS cedula_cliente_menos_frecuente,  menosf.nombre_C  AS nombre_cliente_menos_frecuente,
       masf.cedula_C     AS cedula_cliente_mas_frecuente,    masf.nombre_C    AS nombre_cliente_mas_frecuente
FROM menos_frecuente_T menosf, mas_frecuente_T masf;