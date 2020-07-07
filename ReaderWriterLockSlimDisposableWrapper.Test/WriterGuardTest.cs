// <copyright file="WriterGuardTest.cs" company="Scada International A/S">
// Copyright (c) Scada International A/S. All rights reserved.
// </copyright>

namespace Scada.OneView.DriverTest.Threading
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using ReaderWriterLockSlimDisposableWrapper;
    using Xunit;

    [ExcludeFromCodeCoverage]
    public class WriterGuardTest
    {
        [Fact]
        public void EntersWriteLockOnCreation()
        {
            // Arrange
            var @lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

            // Act
            _ = new WriterGuard(@lock);

            // Assert
            Assert.False(@lock.IsReadLockHeld);
            Assert.True(@lock.IsWriteLockHeld);
            Assert.False(@lock.IsUpgradeableReadLockHeld);
        }

        [Fact]
        public void ExitsWriteLockOnDisposal()
        {
            // Arrange
            var @lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
            var target = new WriterGuard(@lock);

            // Act
            target.Dispose();

            // Assert
            Assert.False(@lock.IsReadLockHeld);
            Assert.False(@lock.IsWriteLockHeld);
            Assert.False(@lock.IsUpgradeableReadLockHeld);
        }
    }
}