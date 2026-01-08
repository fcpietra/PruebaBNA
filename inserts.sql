INSERT INTO Clientes (Id, Nombre, Apellido, Cuit, CreatedAt) 
VALUES (1, 'Lionel', 'Messi', '30500010912', datetime('now'));

INSERT INTO Cuentas (Id, ClienteId, Numero, CodSucursal, Saldo, CreatedAt) 
VALUES (1, 1, '0000123456', '011', 150000.50, datetime('now'));

INSERT INTO Movimientos (Id, CuentaId, Fecha, Tipo, Descripcion, Importe, CreatedAt) 
VALUES (1, 1, datetime('now', '-5 days'), 'HaberesSueldoAcreditado', 'Sueldo Mayo', 200000.00, datetime('now'));

INSERT INTO Movimientos (Id, CuentaId, Fecha, Tipo, Descripcion, Importe, CreatedAt) 
VALUES (2, 1, datetime('now', '-1 days'), 'CompraTarjetaDeDebito', 'Supermercado Coto', 49999.50, datetime('now'));