using BSB.Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace BSB.Data
{
    public class ApplicationDbContext : IdentityDbContext<BSBUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<ProductInShoppingCart> ProductInShoppingCarts { get; set; }
        public virtual DbSet<ProductInOrder> ProductInOrders { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<CommentInPost> CommentInPosts { get; set; }
        public virtual DbSet<CommentInUser> CommentInUsers { get; set; }


        public virtual DbSet<EmailMessage> EmailMessages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<ShoppingCart>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<ProductInShoppingCart>()
                .HasOne(z => z.Product)
                .WithMany(z => z.ProductInShoppingCarts)
                .HasForeignKey(z => z.ShoppingCartId);

            builder.Entity<ProductInShoppingCart>()
                .HasOne(z => z.ShoppingCart)
                .WithMany(z => z.ProductInShoppingCarts)
                .HasForeignKey(z => z.ProductId);


            builder.Entity<ShoppingCart>()
                .HasOne<BSBUser>(z => z.User)
                .WithOne(z => z.UserCart)
                .HasForeignKey<ShoppingCart>(z => z.UserId);

            builder.Entity<ProductInOrder>()
                .HasOne(z => z.Product)
                .WithMany(z => z.ProductInOrders)
                .HasForeignKey(z => z.OrderId);

            builder.Entity<ProductInOrder>()
                .HasOne(z => z.Order)
                .WithMany(z => z.ProductInOrders)
                .HasForeignKey(z => z.ProductId);

            builder.Entity<CommentInPost>()
                .HasOne(z => z.Post)
                .WithMany(z => z.CommentsInPost)
                .HasForeignKey(z => z.CommentId);

            builder.Entity<CommentInUser>()
                .HasOne<BSBUser>(z => z.User)
                .WithMany(z => z.Comments)
                .HasForeignKey(z => z.UserId);

            builder.Entity<Post>()
                .HasOne<BSBUser>(z => z.ByUser)
                .WithMany(z => z.Posts)
                .HasForeignKey(z => z.ByUserId);
        }
    }
    }

