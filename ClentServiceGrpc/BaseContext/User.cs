using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BaseContext;

public partial class User : IEntity
{
    public int Id { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    [ConcurrencyCheck]
    public string Firstname { get; set; } = null!;

    /// <summary>
    /// Фамилия
    /// </summary>
    [ConcurrencyCheck]
    public string? Lastname { get; set; }

    /// <summary>
    /// Отчество
    /// </summary>
    [ConcurrencyCheck]
    public string? Secondname { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    [ConcurrencyCheck]
    public DateOnly? Birthdaydate { get; set; }

    /// <summary>
    /// Наличие детей
    /// </summary>
    [ConcurrencyCheck]
    public bool? Children { get; set; }

    /// <summary>
    /// Пол
    /// </summary>
    //[ConcurrencyCheck]
    //public sex? Sex { get; set; }

    public enum sex{
        мужчина,
        женщина,
        иное

    }
}
