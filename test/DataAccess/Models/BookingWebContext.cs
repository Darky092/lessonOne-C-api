using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

public partial class BookingWebContext : DbContext
{
    public BookingWebContext()
    {
    }

    public BookingWebContext(DbContextOptions<BookingWebContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Бронирование> Бронированиеs { get; set; }

    public virtual DbSet<Клиенты> Клиентыs { get; set; }

    public virtual DbSet<Комуникации> Комуникацииs { get; set; }

    public virtual DbSet<Номер> Номерs { get; set; }

    public virtual DbSet<Отель> Отельs { get; set; }

    public virtual DbSet<Тариф> Тарифs { get; set; }

    public virtual DbSet<ТарифыНомеров> ТарифыНомеровs { get; set; }

    public virtual DbSet<Услуга> Услугаs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-GJOMEEO;Database=booking_web;Trusted_Connection=true;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Бронирование>(entity =>
        {
            entity.HasKey(e => e.КодБронирования);

            entity.ToTable("Бронирование");

            entity.Property(e => e.КодБронирования).HasColumnName("Код_Бронирования");
            entity.Property(e => e.ДатаВъезда).HasColumnName("Дата_Въезда");
            entity.Property(e => e.ДатаВыезда).HasColumnName("Дата_Выезда");
            entity.Property(e => e.ФактОплаты).HasColumnName("Факт_Оплаты");

            entity.HasMany(d => d.КодКлиентаs).WithMany(p => p.КодБронированияs)
                .UsingEntity<Dictionary<string, object>>(
                    "БронированиеКлиенты",
                    r => r.HasOne<Клиенты>().WithMany()
                        .HasForeignKey("КодКлиента")
                        .HasConstraintName("Бронирует"),
                    l => l.HasOne<Бронирование>().WithMany()
                        .HasForeignKey("КодБронирования")
                        .HasConstraintName("R12"),
                    j =>
                    {
                        j.HasKey("КодБронирования", "КодКлиента");
                        j.ToTable("Бронирование_Клиенты");
                        j.IndexerProperty<int>("КодБронирования").HasColumnName("Код_Бронирования");
                        j.IndexerProperty<int>("КодКлиента").HasColumnName("Код_Клиента");
                    });

            entity.HasMany(d => d.ТарифыНомеровs).WithMany(p => p.КодБронированияs)
                .UsingEntity<Dictionary<string, object>>(
                    "БронированиеНомера",
                    r => r.HasOne<ТарифыНомеров>().WithMany()
                        .HasForeignKey("КодТарифа", "КодНомера")
                        .HasConstraintName("R15"),
                    l => l.HasOne<Бронирование>().WithMany()
                        .HasForeignKey("КодБронирования")
                        .HasConstraintName("R11"),
                    j =>
                    {
                        j.HasKey("КодБронирования", "КодТарифа", "КодНомера");
                        j.ToTable("Бронирование_Номера");
                        j.IndexerProperty<int>("КодБронирования").HasColumnName("Код_Бронирования");
                        j.IndexerProperty<int>("КодТарифа").HasColumnName("Код_Тарифа");
                        j.IndexerProperty<int>("КодНомера").HasColumnName("Код_Номера");
                    });
        });

        modelBuilder.Entity<Клиенты>(entity =>
        {
            entity.HasKey(e => e.КодКлиента);

            entity.ToTable("Клиенты");

            entity.Property(e => e.КодКлиента).HasColumnName("Код_Клиента");
            entity.Property(e => e.КемВыданПаспорт).HasColumnName("Кем_Выдан_Паспорт");
            entity.Property(e => e.Пол)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.СерияПаспорта).HasColumnName("Серия_Паспорта");
        });

        modelBuilder.Entity<Комуникации>(entity =>
        {
            entity.HasKey(e => e.КодКоммуникации);

            entity.ToTable("Комуникации");

            entity.Property(e => e.КодКоммуникации).HasColumnName("Код_коммуникации");
            entity.Property(e => e.НазваниеКоммуникации).HasColumnName("Название_Коммуникации");

            entity.HasMany(d => d.КодНомераs).WithMany(p => p.КодКоммуникацииs)
                .UsingEntity<Dictionary<string, object>>(
                    "НаличиеКоммуникаций",
                    r => r.HasOne<Номер>().WithMany()
                        .HasForeignKey("КодНомера")
                        .HasConstraintName("Содержит"),
                    l => l.HasOne<Комуникации>().WithMany()
                        .HasForeignKey("КодКоммуникации")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Имеется"),
                    j =>
                    {
                        j.HasKey("КодКоммуникации", "КодНомера");
                        j.ToTable("Наличие_Коммуникаций");
                        j.IndexerProperty<int>("КодКоммуникации").HasColumnName("Код_коммуникации");
                        j.IndexerProperty<int>("КодНомера").HasColumnName("Код_Номера");
                    });
        });

        modelBuilder.Entity<Номер>(entity =>
        {
            entity.HasKey(e => e.КодНомера);

            entity.ToTable("Номер");

            entity.HasIndex(e => e.КодОтеля, "IX_R2");

            entity.Property(e => e.КодНомера).HasColumnName("Код_Номера");
            entity.Property(e => e.КодОтеля).HasColumnName("Код_Отеля");
            entity.Property(e => e.КоличествоВанныхКомнат).HasColumnName("Количество_Ванных_Комнат");
            entity.Property(e => e.КоличествоСпальныхМест).HasColumnName("Количество_Спальных_Мест");

            entity.HasOne(d => d.КодОтеляNavigation).WithMany(p => p.Номерs)
                .HasForeignKey(d => d.КодОтеля)
                .HasConstraintName("R2");
        });

        modelBuilder.Entity<Отель>(entity =>
        {
            entity.HasKey(e => e.КодОтеля);

            entity.ToTable("Отель");

            entity.Property(e => e.КодОтеля).HasColumnName("Код_Отеля");
            entity.Property(e => e.Город).HasMaxLength(100);
            entity.Property(e => e.НазваниеОтеля)
                .HasMaxLength(100)
                .HasColumnName("Название_Отеля");
            entity.Property(e => e.НомерДома).HasColumnName("Номер_Дома");
        });

        modelBuilder.Entity<Тариф>(entity =>
        {
            entity.HasKey(e => e.КодТарифа);

            entity.ToTable("Тариф");

            entity.Property(e => e.КодТарифа).HasColumnName("Код_Тарифа");
            entity.Property(e => e.НазваниеТарифа).HasColumnName("Название_Тарифа");
            entity.Property(e => e.ЦенаТарифа)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Цена_Тарифа");
        });

        modelBuilder.Entity<ТарифыНомеров>(entity =>
        {
            entity.HasKey(e => new { e.КодТарифа, e.КодНомера });

            entity.ToTable("Тарифы_Номеров");

            entity.Property(e => e.КодТарифа).HasColumnName("Код_Тарифа");
            entity.Property(e => e.КодНомера).HasColumnName("Код_Номера");

            entity.HasOne(d => d.КодНомераNavigation).WithMany(p => p.ТарифыНомеровs)
                .HasForeignKey(d => d.КодНомера)
                .HasConstraintName("R5");

            entity.HasOne(d => d.КодТарифаNavigation).WithMany(p => p.ТарифыНомеровs)
                .HasForeignKey(d => d.КодТарифа)
                .HasConstraintName("R3");
        });

        modelBuilder.Entity<Услуга>(entity =>
        {
            entity.HasKey(e => e.КодУслуги);

            entity.ToTable("Услуга");

            entity.Property(e => e.КодУслуги).HasColumnName("Код_Услуги");
            entity.Property(e => e.НазваниеУслуги)
                .HasMaxLength(100)
                .HasColumnName("Название_Услуги");
            entity.Property(e => e.ЦенаУслуги)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Цена_Услуги");

            entity.HasMany(d => d.КодТарифаs).WithMany(p => p.КодУслугиs)
                .UsingEntity<Dictionary<string, object>>(
                    "УслугиТарифов",
                    r => r.HasOne<Тариф>().WithMany()
                        .HasForeignKey("КодТарифа")
                        .HasConstraintName("R8"),
                    l => l.HasOne<Услуга>().WithMany()
                        .HasForeignKey("КодУслуги")
                        .HasConstraintName("R7"),
                    j =>
                    {
                        j.HasKey("КодУслуги", "КодТарифа");
                        j.ToTable("Услуги_Тарифов");
                        j.IndexerProperty<int>("КодУслуги").HasColumnName("Код_Услуги");
                        j.IndexerProperty<int>("КодТарифа").HasColumnName("Код_Тарифа");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
