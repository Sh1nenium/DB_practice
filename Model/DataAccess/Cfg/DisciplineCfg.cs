﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.DataAccess.Cfg
{
    public class DisciplineCfg : IEntityTypeConfiguration<Discipline>
    {
        public void Configure(EntityTypeBuilder<Discipline> builder)
        {
            builder.HasMany<Task>()
                .WithOne();

            builder.HasMany<TrainingManual>()
                .WithOne();
        }
    }
}