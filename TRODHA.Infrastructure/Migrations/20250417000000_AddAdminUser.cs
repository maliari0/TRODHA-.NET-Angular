﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Security.Cryptography;
using System.Text;

#nullable disable

namespace TRODHA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Admin kullanıcısı için şifre hash'i oluştur
            CreatePasswordHash("admin123", out string passwordHash, out string passwordSalt);

            // Admin kullanıcısını ekle - SQL komutunu doğrudan çalıştır
            var birthDate = DateTime.Now.ToString("yyyy-MM-dd");
            var createdAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var updatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // SQL Injection'a karşı koruma için parametreleri ayrı ayrı ekleyelim
            migrationBuilder.Sql($@"
                IF NOT EXISTS (SELECT 1 FROM Users WHERE Email = 'admin@admin.com')
                BEGIN
                    INSERT INTO Users (Email, PasswordHash, PasswordSalt, FirstName, LastName, BirthDate, CreatedAt, UpdatedAt, LastLoginAt)
                    VALUES ('admin@admin.com', '{passwordHash.Replace("'", "''")}', '{passwordSalt.Replace("'", "''")}', 'Admin', 'User', '{birthDate}', '{createdAt}', '{updatedAt}', NULL)
                END
            ");

            // Yorum satırı ekleme.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Admin kullanıcısını sil
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM Users WHERE Email = 'admin@admin.com')
                BEGIN
                    DELETE FROM Users WHERE Email = 'admin@admin.com'
                END
            ");
        }

        // Şifre hash'i oluşturmak için yardımcı metod
        private static void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = Convert.ToBase64String(hmac.Key);
                passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }
    }
}
