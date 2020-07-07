// <copyright file="ReaderGuardTest.cs" company="Scada International A/S">
// Copyright (c) Scada International A/S. All rights reserved.
// </copyright>

namespace Scada.OneView.DriverTest.Threading
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using ReaderWriterLockSlimDisposableWrapper;
    using Xunit;

    [ExcludeFromCodeCoverage]
    public class ReaderGuardTest
    {
        [Fact]
        public void EntersReadLockOnCreation()
        {
            // Arrange
            var @lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

            // Act
            _ = new ReaderGuard(@lock);

            // Assert
            Assert.True(@lock.IsReadLockHeld);
            Assert.False(@lock.IsWriteLockHeld);
            Assert.False(@lock.IsUpgradeableReadLockHeld);
        }

        [Fact]
        public void ExitsReadLockOnDisposal()
        {
            // Arrange
            var @lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
            var target = new ReaderGuard(@lock);

            // Act
            target.Dispose();

            // Assert
            Assert.False(@lock.IsReadLockHeld);
            Assert.False(@lock.IsWriteLockHeld);
            Assert.False(@lock.IsUpgradeableReadLockHeld);
        }
    }
}