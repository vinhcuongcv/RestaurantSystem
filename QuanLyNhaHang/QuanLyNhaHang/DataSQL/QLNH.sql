-- Tạo database
CREATE DATABASE QuanLyNhaHang
GO

USE QuanLyNhaHang;
GO

-- Bảng bàn ăn
CREATE TABLE TableFood
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) DEFAULT N'Chưa đặt tên',
	status NVARCHAR(100) NOT NULL DEFAULT N'Trống'  -- Trống, có người
);
GO

-- Bảng tài khoản
CREATE TABLE dbo.Account
(
	UserName NVARCHAR(100) PRIMARY KEY,
	DisplayName NVARCHAR(100) NOT NULL,
	PassWord NVARCHAR(100) NOT NULL DEFAULT 0,
	Type INT NOT NULL DEFAULT 0 -- 1: admin, 0: staff
);
GO

INSERT INTO Account
VALUES 
('VC', 'VinhCuong',1 ,1),
('PT', 'PhuongTram', 1, 1),
('NA', 'NgocAnh', 1, 1),
('VCStaff', 'StaffVinhCuong', 1,0),
('PTStaff', 'StaffPhuongTram', 1, 0),
('NAStaff', 'StaffNgocAnh', 1, 0);



-- Bảng danh mục món ăn
CREATE TABLE FoodCategory
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên'
);
GO

-- Bảng món ăn
CREATE TABLE Food
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
	idCategory INT NOT NULL,
	price FLOAT NOT NULL DEFAULT 0,
	FOREIGN KEY (idCategory) REFERENCES dbo.FoodCategory(id)
);
GO

-- Bảng hóa đơn
CREATE TABLE Bill
(
	id INT IDENTITY PRIMARY KEY,
	dateCheckIn DATE NOT NULL DEFAULT GETDATE(),
	dateCheckOut DATE,
	idTable INT NOT NULL,
	status INT NOT NULL DEFAULT 0,  -- Đã thanh toán: 1, chưa thanh toán: 0
	FOREIGN KEY (idTable) REFERENCES dbo.TableFood(id),
	discount FLOAT NOT NULL DEFAULT 0,
	totalPrice FLOAT 
);
GO

-- Bảng chi tiết hóa đơn
CREATE TABLE BillInfo
(
	id INT IDENTITY PRIMARY KEY,
	idBill INT NOT NULL,
	idFood INT NOT NULL,
	count INT NOT NULL DEFAULT 0,
	FOREIGN KEY (idBill) REFERENCES dbo.Bill(id),
	FOREIGN KEY (idFood) REFERENCES dbo.Food(id)
);
GO

-- Thêm 10 bàn ăn
INSERT INTO TableFood (name, status)
VALUES
(N'Bàn 1', N'Trống'), (N'Bàn 2', N'Trống'), (N'Bàn 3', N'Trống'),
(N'Bàn 4', N'Trống'), (N'Bàn 5', N'Trống'), (N'Bàn 6', N'Trống'),
(N'Bàn 7', N'Trống'), (N'Bàn 8', N'Trống'), (N'Bàn 9', N'Trống'),
(N'Bàn 10', N'Trống');
GO

DBCC CHECKIDENT ('TableFood', RESEED, 10);
GO

DECLARE @i INT = 11;
WHILE @i <= 28
BEGIN 
    INSERT INTO dbo.TableFood (name, status)
    VALUES (N'Bàn ' + CAST(@i AS NVARCHAR(100)), N'Trống');
    SET @i = @i + 1;
END
GO


-- Tạo stored procedure lấy danh sách bàn
CREATE PROC USP_GetTableList
AS
	SELECT * FROM dbo.TableFood;
GO

-- Thêm danh mục món ăn
INSERT dbo.FoodCategory (name)
VALUES 
(N'Hải sản'),
(N'Đồ nướng'),
(N'Đồ nước'),
(N'Đồ chiên xào'),
(N'Thức uống giải khát');

-- Thêm món ăn
INSERT dbo.Food (name, idCategory, price)
VALUES
-- Hải sản
(N'Mực 1 nắng', 1, 120000),
(N'Nghêu hấp xả', 1, 80000),
(N'Bạch tuộc xào sả ớt', 1, 150000),
(N'Tôm tít sốt tỏi', 1, 200000),
(N'Cua cà mau hấp bia', 1, 320000),

-- Đồ nướng
(N'Thăn vai bò mỹ sốt Galbi', 2, 300000),
(N'Ba chỉ bò mỹ sốt mật ong', 2, 280000),
(N'Nạc vai heo', 2, 180000),
(N'Dẻ sườn bò mỹ sốt đặc biệt', 2, 360000),
(N'Ba chỉ heo sốt Gogihouse', 2, 200000),

-- Đồ nước (lẩu, phở, bún)
(N'Lẩu cá bớp', 3, 220000),
(N'Lẩu thái Tomyum', 3, 200000),
(N'Lẩu cá ngừ', 3, 400000),
(N'Lẩu cay tứ xuyên', 3, 360000),
(N'Lẩu kim chi Hàn Quốc', 3, 300000),
(N'Phở Hà Nội đặc biệt', 3, 100000),
(N'Bún khô Gia Lai', 3, 60000),

-- Chiên xào
(N'Bánh takoyaki', 4, 40000),
(N'Kimbap', 4, 45000),
(N'Gà rán sốt galbi', 4, 120000),
(N'Bánh gạo cay hàn quốc', 4, 80000),
(N'Cơm chiên hải sản', 4, 55000),
(N'Cơm chiên bò lúc lắc', 4, 52000),
(N'Mỳ xào bò', 4, 50000),
(N'Bánh phở xào thập cẩm', 4, 50000),

-- Thức uống giải khát
(N'Coca Cola', 5, 18000),
(N'Pepsi', 5, 18000),
(N'Mirinda', 5, 18000),
(N'Cà phê sữa đá', 5, 25000),
(N'Cà phê đen', 5, 22000),
(N'Trà đào', 5, 28000),
(N'Trà sữa kem trứng nướng', 5, 38000),
(N'Trà chanh', 5, 18000),
(N'Trà gừng', 5, 18000),
(N'Bia Tiger', 5, 22000),
(N'Bia Heneiken', 5, 22000),
(N'Bia Sài Gòn', 5, 22000);
GO

-- Thêm Bill
INSERT dbo.Bill (dateCheckIn, dateCheckOut, idTable, status)
VALUES 
(GETDATE(), NULL, 1, 0),
(GETDATE(), NULL, 2, 0),
(GETDATE(), GETDATE(), 3, 1);

-- Thêm BillInfo
INSERT dbo.BillInfo (idBill, idFood, count)
VALUES 
(1, 1, 2),
(1, 2, 2),
(3, 18, 2);
GO

-- Tạo thủ tục thêm Bill
CREATE PROCEDURE USP_InsertBill
	@idTable INT
AS
BEGIN 
	INSERT dbo.Bill (dateCheckIn, dateCheckOut, idTable, status, discount)
	VALUES (GETDATE(), NULL, @idTable, 0, 0);
END;
GO

-- Tạo thủ tục thêm BillInfo có xử lý cập nhật
CREATE PROCEDURE USP_InsertBillInfo
	@idBill INT,
	@idFood INT,
	@Count INT
AS
BEGIN
	DECLARE @isExistBillInfo INT;
	DECLARE @FoodCount INT;

	SELECT @isExistBillInfo = b.id, @FoodCount = b.count
	FROM dbo.BillInfo AS b
	WHERE idBill = @idBill AND idFood = @idFood;

	IF (@isExistBillInfo > 0)
	BEGIN
		DECLARE @newCount INT = @FoodCount + @Count;
		IF (@newCount > 0)
			UPDATE dbo.BillInfo SET count = @newCount 
			WHERE idBill = @idBill AND idFood = @idFood;
		ELSE
			DELETE dbo.BillInfo 
			WHERE idBill = @idBill AND idFood = @idFood;
	END
	ELSE
	BEGIN
		INSERT dbo.BillInfo (idBill, idFood, count)
		VALUES (@idBill, @idFood, @Count);
	END
END;
GO

CREATE TRIGGER UTG_UpdateBillInfo
ON dbo.BillInfo FOR INSERT, UPDATE
AS
BEGIN
	DECLARE @idBill INT  
  
    SELECT @idBill = idBill FROM Inserted  
  
    DECLARE @idTable INT  
  
    SELECT @idTable = idTable FROM dbo.Bill WHERE id = @idBill AND status = 0  
  
    UPDATE dbo.TableFood SET status = N'Có người' WHERE id = @idTable  
END;  
GO

CREATE TRIGGER UTG_UpdateBill
ON dbo.Bill FOR UPDATE
AS  
BEGIN  
    DECLARE @idBill INT;
    SELECT @idBill = id FROM Inserted;
  
    DECLARE @idTable INT;
    SELECT @idTable = idTable FROM dbo.Bill WHERE id = @idBill;
  
    DECLARE @count INT = 0;
    SELECT @count = COUNT(*) FROM dbo.Bill WHERE idTable = @idTable AND status = 0;
  
    IF (@count = 0)  
        UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable;  
END;
GO

CREATE PROCEDURE USP_SwitchTable 
    @idTable1 INT, 
    @idTable2 INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @idFirstBill INT;
        DECLARE @idSecondBill INT;

        -- Lấy hóa đơn hiện tại của hai bàn
        SELECT @idFirstBill = id FROM dbo.Bill WHERE idTable = @idTable1 AND status = 0;
        SELECT @idSecondBill = id FROM dbo.Bill WHERE idTable = @idTable2 AND status = 0;

        -- Nếu bàn chưa có hóa đơn, tạo mới
        IF (@idFirstBill IS NULL)
        BEGIN
            INSERT INTO dbo.Bill (dateCheckIn, dateCheckOut, idTable, status)
            VALUES (GETDATE(), NULL, @idTable1, 0);
            SELECT @idFirstBill = SCOPE_IDENTITY();
        END;

        IF (@idSecondBill IS NULL)
        BEGIN
            INSERT INTO dbo.Bill (dateCheckIn, dateCheckOut, idTable, status)
            VALUES (GETDATE(), NULL, @idTable2, 0);
            SELECT @idSecondBill = SCOPE_IDENTITY();
        END;

        -- Lưu lại BillInfo của từng hóa đơn vào bảng tạm
        SELECT * INTO #TempBillInfo1 FROM dbo.BillInfo WHERE idBill = @idFirstBill;
        SELECT * INTO #TempBillInfo2 FROM dbo.BillInfo WHERE idBill = @idSecondBill;

        -- Xóa dữ liệu cũ trong BillInfo của hai hóa đơn
        DELETE FROM dbo.BillInfo WHERE idBill IN (@idFirstBill, @idSecondBill);

        -- Chuyển BillInfo giữa hai hóa đơn
        INSERT INTO dbo.BillInfo (idBill, idFood, count)
        SELECT @idSecondBill, idFood, count FROM #TempBillInfo1;

        INSERT INTO dbo.BillInfo (idBill, idFood, count)
        SELECT @idFirstBill, idFood, count FROM #TempBillInfo2;

        -- Cập nhật lại trạng thái bàn
        IF EXISTS (SELECT 1 FROM dbo.BillInfo WHERE idBill = @idFirstBill)
            UPDATE dbo.TableFood SET status = N'Có người' WHERE id = @idTable1;
        ELSE
            UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable1;

        IF EXISTS (SELECT 1 FROM dbo.BillInfo WHERE idBill = @idSecondBill)
            UPDATE dbo.TableFood SET status = N'Có người' WHERE id = @idTable2;
        ELSE
            UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable2;

        -- Xóa bảng tạm
        DROP TABLE #TempBillInfo1;
        DROP TABLE #TempBillInfo2;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- In lỗi nếu có
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH;
END;
GO

CREATE PROC USP_GetListBillByDate
    @dateCheckIn DATE, 
    @dateCheckOut DATE
AS
BEGIN
    SELECT 
        t.name AS [Tên bàn], 
        dateCheckIn AS [Ngày vào], 
        dateCheckOut AS [Ngày ra],
        CAST(b.discount AS VARCHAR(10)) + N'%' AS [Giảm giá],
        FORMAT(CAST(b.totalPrice AS MONEY), 'N0', 'vi-VN') + N' đ' AS [Tổng tiền]
    FROM dbo.Bill AS b 
    JOIN dbo.TableFood AS t ON b.idTable = t.id
    JOIN dbo.BillInfo AS bi ON b.id = bi.idBill
    WHERE b.status = 1 
    AND dateCheckIn >= @dateCheckIn 
    AND dateCheckOut <= @dateCheckOut;
END;
GO

CREATE PROC USP_UpdateAccount
	@userName NVARCHAR(100), 
	@displayName NVARCHAR(100), 
	@password NVARCHAR(100), 
	@newPassword NVARCHAR(100)
AS
BEGIN
    DECLARE @isRightPass INT = 0

    SELECT @isRightPass = COUNT(*) FROM dbo.Account WHERE USERName = @userName AND PassWord = @password

    IF (@isRightPass = 1)
    BEGIN
        IF (@newPassword = NULL OR @newPassword = '')
        BEGIN
            UPDATE dbo.Account SET DisplayName = @displayName WHERE UserName = @userName
        END
        ELSE
            UPDATE dbo.Account SET DisplayName = @displayName, PassWord = @newPassword WHERE UserName = @userName
    END
END;	
GO

CREATE TRIGGER UTG_DeleteBillInfo
ON dbo.BillInfo FOR DELETE
AS
BEGIN 
	DECLARE @idTable INT;

	-- Lấy danh sách các Bill vừa bị xóa BillInfo
	DECLARE @billIDs TABLE (idBill INT);

	INSERT INTO @billIDs (idBill)
	SELECT DISTINCT idBill FROM deleted;

	-- Lặp qua từng Bill, kiểm tra nếu không còn món thì set bàn thành Trống
	UPDATE TableFood
	SET status = N'Trống'
	WHERE id IN (
		SELECT b.idTable
		FROM dbo.Bill b
		JOIN @billIDs ids ON b.id = ids.idBill
		WHERE b.status = 0
		AND NOT EXISTS (
			SELECT 1 FROM dbo.BillInfo bi WHERE bi.idBill = b.id
		)
	);
END;
GO

CREATE FUNCTION [dbo].[fuConvertToUnsign1]
(
    @strInput NVARCHAR(4000)
)
RETURNS NVARCHAR(4000)
AS
BEGIN
    IF @strInput IS NULL
        RETURN NULL;

    -- Dạng thường (chữ thường)
    SET @strInput = LOWER(@strInput);

    -- A
    SET @strInput = REPLACE(@strInput, N'áàảãạăắằẳẵặâấầẩẫậ', N'a');
    -- E
    SET @strInput = REPLACE(@strInput, N'éèẻẽẹêếềểễệ', N'e');
    -- I
    SET @strInput = REPLACE(@strInput, N'íìỉĩị', N'i');
    -- O
    SET @strInput = REPLACE(@strInput, N'óòỏõọôốồổỗộơớờởỡợ', N'o');
    -- U
    SET @strInput = REPLACE(@strInput, N'úùủũụưứừửữự', N'u');
    -- Y
    SET @strInput = REPLACE(@strInput, N'ýỳỷỹỵ', N'y');
    -- D
    SET @strInput = REPLACE(@strInput, N'đ', N'd');

    RETURN @strInput;
END;
GO

CREATE PROC USP_GetListBillByDateAndPage
    @checkIn DATE,
    @checkOut DATE,
    @page INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @pageRows INT = 13;
    DECLARE @startRow INT = (@page - 1) * @pageRows + 1;
    DECLARE @endRow INT = @page * @pageRows;

    WITH BillShow AS (
        SELECT 
            ROW_NUMBER() OVER (ORDER BY b.dateCheckIn ASC, b.id ASC) AS RowNum,
            b.id AS [Mã hóa đơn],
            t.name AS [Tên bàn], 
            b.dateCheckIn AS [Ngày vào], 
            b.dateCheckOut AS [Ngày ra],
            CAST(b.discount AS VARCHAR(10)) + N'%' AS [Giảm giá],
            FORMAT(CAST(b.totalPrice AS MONEY), 'N0', 'vi-VN') + N' đ' AS [Tổng tiền]
        FROM (
            SELECT DISTINCT id, idTable, dateCheckIn, dateCheckOut, discount, totalPrice, status
            FROM dbo.Bill
            WHERE 
                status = 1 
                AND dateCheckIn >= @checkIn 
                AND dateCheckOut <= @checkOut
        ) AS b
        INNER JOIN dbo.TableFood AS t ON t.id = b.idTable
    )
    SELECT 
        [Mã hóa đơn], [Tên bàn], [Ngày vào], [Ngày ra], [Giảm giá], [Tổng tiền]
    FROM BillShow
    WHERE RowNum BETWEEN @startRow AND @endRow;
END
GO

CREATE PROC USP_GetNumBillByDate
	@checkIn DATE, 
	@checkOut DATE
AS
BEGIN
	SELECT COUNT(*)
	FROM dbo.Bill AS b, dbo.TableFood AS t
	WHERE dateCheckIn >= @checkIn 
		AND dateCheckOut <= @checkOut 
		AND b.status = 1
		AND t.id = b.idTable
END;
GO

CREATE TRIGGER UTG_DeleteFoodWhenCategoryDeleted
ON FoodCategory
INSTEAD OF DELETE
AS
BEGIN
	SET NOCOUNT ON;

	-- Xóa tất cả món ăn thuộc danh mục bị xóa
    DELETE FROM Food
    WHERE idCategory IN (SELECT id FROM DELETED);

    -- Xóa danh mục
    DELETE FROM FoodCategory
    WHERE id IN (SELECT id FROM DELETED);
END;
GO

CREATE PROCEDURE USP_GetBillInfoByBillID
	@billId INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		f.name AS [Tên món ăn],
		bi.count AS [Số lượng],
        FORMAT(
            CAST(bi.count * f.price AS MONEY),
            'N0',
            'vi-VN') + N' đ' AS [Thành tiền]
    FROM dbo.BillInfo AS bi
    INNER JOIN dbo.Food AS f ON bi.idFood = f.id
    WHERE bi.idBill = @billId;
END;
GO

CREATE PROCEDURE USP_InsertAccount
    @username NVARCHAR(50),
    @displayname NVARCHAR(100),
    @password NVARCHAR(100),
    @type INT
AS
BEGIN
    -- Kiểm tra username đã tồn tại chưa
    IF EXISTS (SELECT 1 FROM dbo.Account WHERE UserName = @username)
    BEGIN
        -- Trả về lỗi nếu trùng username
        RETURN;
    END

    -- Thêm tài khoản nếu không trùng
    INSERT INTO Account (UserName, DisplayName, PassWord, Type)
    VALUES (@username, @displayname, @password, @type);
END;

DELETE FROM dbo.BillInfo WHERE id = 1
DELETE FROM dbo.BillInfo WHERE id = 2

