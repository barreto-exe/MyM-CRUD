
ALTER TABLE facturas
ADD CONSTRAINT jerarquia2_facturas
CHECK ((tipo_fact='S' AND EXIST (facturas_servicio)
                      AND NOT EXIST (facturas_tienda))
        OR
        (tipo_fact='T' AND EXIST (facturas_tienda)
                       AND NOT EXIST (facturas_servicio)));


ALTER TABLE productos
ADD CONSTRAINT jerarquia2_productos
CHECK ((tipo_p='T' AND EXIST (tienda_productos)
                   AND NOT EXIST (eco_productos))
        OR
        (tipo_p='E' AND EXIST (eco_productos)
                    AND NOT EXIST (tienda_productos)));

                    