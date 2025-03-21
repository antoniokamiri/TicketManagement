using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;
public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.Property(t => t.File_Name).HasMaxLength(80).IsRequired();
        builder.HasIndex(p => p.File_Name).IsUnique();
        builder.Property(t => t.Server_File_Name).HasMaxLength(150);
    }
}

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(t => t.Name).HasMaxLength(80).IsRequired();
        builder.HasIndex(p => p.Name).IsUnique();

    }
}

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(t => t.Name).HasMaxLength(80).IsRequired();
        builder.HasIndex(p => p.Name).IsUnique();
    }
}

public class PriorityConfiguration : IEntityTypeConfiguration<Priority>
{
    public void Configure(EntityTypeBuilder<Priority> builder)
    {
        builder.Property(t => t.Name).HasMaxLength(80).IsRequired();
        builder.HasIndex(p => p.Name).IsUnique();
    }
}

public class DiscussionConfiguration : IEntityTypeConfiguration<Discussion>
{
    public void Configure(EntityTypeBuilder<Discussion> builder)
    {
        builder.Property(t => t.Message).HasMaxLength(250).IsRequired();
        //builder.HasIndex(p => p.InvoiceNo).IsUnique();

        builder.HasOne(t => t.Ticket).WithMany().HasForeignKey(x => x.Ticket_Id).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(t => t.User).WithMany().HasForeignKey(x => x.User_Id).OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(t => t.Attachments).WithOne(p => p.Discussion).HasForeignKey(x => x.Discussion_Id).OnDelete(DeleteBehavior.NoAction);

    }
}

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.Property(t => t.Summary).HasMaxLength(250).IsRequired();
        builder.Property(t => t.Description).HasMaxLength(250);
        builder.Property(t => t.Status).HasMaxLength(20).IsRequired();
        //builder.HasIndex(p => p.InvoiceNo).IsUnique();

        builder.HasOne(t => t.Assigned_To).WithMany().HasForeignKey(x => x.Assigned_To_Id).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(t => t.Raised_By).WithMany().HasForeignKey(x => x.Raised_By_Id).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(t => t.Product).WithMany().HasForeignKey(x => x.Product_Id).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(t => t.Category).WithMany().HasForeignKey(x => x.Category_Id).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(t => t.Priority).WithMany().HasForeignKey(x => x.Priority_Id).OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(t => t.Attachments).WithOne(p => p.Ticket).HasForeignKey(x => x.Ticket_Id).OnDelete(DeleteBehavior.NoAction);

    }
}